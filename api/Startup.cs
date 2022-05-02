using api;
using api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Logging.API
{
  public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //DatabaseLogs log = new DatabaseLogs();
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(log);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RemoteLoggerContext>(options => options.EnableSensitiveDataLogging(true));
            services.AddDbContext<RemoteLoggerIdentityContext>(options => options.EnableSensitiveDataLogging(true));  

            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
              options.Password.RequireDigit = false;
              options.Password.RequireLowercase = false;
              options.Password.RequireUppercase = false;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequiredLength = 4;

              options.SignIn.RequireConfirmedEmail = false;
              options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<RemoteLoggerIdentityContext>().AddDefaultTokenProviders();

            services.AddSingleton<IJwtService, JwtService>();

            services.AddControllers();

            SymmetricSecurityKey key = new SymmetricSecurityKey(Configuration.GetValue<byte[]>("EncryptionKeys:AesKey"));

            services.AddAuthentication(options =>
            {
              options.DefaultAuthenticateScheme = "JwtBearer";
              options.DefaultChallengeScheme = "JwtBearer";
            })
            
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {              
              jwtBearerOptions.TokenValidationParameters =
                  new TokenValidationParameters
                  {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = "RemoteLoggerUsers",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromHours(24)
                  };
              jwtBearerOptions.Events =
                  new JwtBearerEvents
                  {
                    OnAuthenticationFailed = context =>
                    {
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                        context.Response.Headers.Add("Token-Expired", "true");
                      }
                      return Task.CompletedTask;
                    }
                  };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
                 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
