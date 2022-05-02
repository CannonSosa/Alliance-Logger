using api.Interfaces;
using api.ViewModels;
using Logging.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace api.Controllers
{
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    
    public UserController(UserManager<User> userManager,
                          IJwtService jwtService)
    {
      _userManager = userManager;
      _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
      Boolean result;

      if (ModelState.IsValid)
      {

        //This code is to manually create a new User

        /*
        User newUser = new User
        {
          UserName = model.UserName,
          NormalizedUserName = model.UserName.Trim().ToUpper(),
          EmailConfirmed = false,
        };

        IdentityResult createUserResult = await _userManager.CreateAsync(newUser, model.Password);

        result = createUserResult.Succeeded;
        
        if (result)
        {
          return Ok();
        }
        else
        {
          return BadRequest();
        }
        */
        User userToVerify = await _userManager.FindByNameAsync(model.UserName.Trim().ToUpper());

        if (userToVerify == null)
        {
          throw new InvalidCredentialException($"The user {model.UserName} was not found the proper arguments were not provided.");
        }
       
        Boolean checkPasswordResult = await _userManager.CheckPasswordAsync(userToVerify, model.Password);

        if (checkPasswordResult)
        {
          IList<String> userAssignedRoles = _userManager.GetRolesAsync(userToVerify).Result;

          String jwt = _jwtService.BuildJwtToken(userToVerify.Id.ToString(), userToVerify.UserName, "RemoteLoggerUsers", DateTime.UtcNow.AddHours(72), userAssignedRoles.FirstOrDefault()); ;

          return Ok(jwt);
        }
        else
        {
          throw new InvalidCredentialException($"The user {model.UserName} has invalid credentials.");
        }

      }

      return BadRequest();
    }
  }
}
