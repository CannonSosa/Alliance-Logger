using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "Admin")]
  [Route("api/[controller]")]
  public class RoleController : ControllerBase
  {
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleController (RoleManager<IdentityRole<int>> roleManager)
    {
      _roleManager = roleManager;
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole([FromBody] RoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        IdentityRole<int> identityRole = new IdentityRole<int>
        {
          Name = model.RoleName,
          NormalizedName = model.RoleName.Trim().ToUpper()
        };

        IdentityResult result = await _roleManager.CreateAsync(identityRole);

        if (result.Succeeded)
        {
          return Ok();
        }

        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }

      return BadRequest(model);
    }
  }
}
