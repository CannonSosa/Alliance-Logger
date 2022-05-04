using Logging.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Logging.API
{
  public class RemoteLoggerIdentityContext: IdentityDbContext<User, IdentityRole<int>, int>
  {
    public RemoteLoggerIdentityContext(DbContextOptions<RemoteLoggerIdentityContext> options) : base(options)
    {
      Database.SetCommandTimeout(0);
    }
    public virtual DbSet<User> DatabaseUsers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.EnableSensitiveDataLogging(true);

      if (!optionsBuilder.IsConfigured)
      {
        //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
#if DEBUG
         optionsBuilder.UseSqlServer(@"server=DESKTOP-L7N9NMJ\SQLEXPRESS;database=RemoteLogger;user id=devlogin;pwd=Benton$42025;");
#else
         optionsBuilder.UseSqlServer(@"server=MSUWEBB4MS;database=RemoteLogger;user id=sa;pwd=Alliance&*****;");
#endif
      }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<User>(e =>
      {
        e.Property(p => p.Id)
        .HasColumnName("UserID");

        e.ToTable("Users");
      });
      
    }   
  }
}
