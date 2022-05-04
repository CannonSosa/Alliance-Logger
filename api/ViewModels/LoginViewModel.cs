using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
  public class LoginViewModel
  {
    [Required]   
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    //[Display(Name = "Remember me")]
    //public bool RememberMe { get; set; }
  }
}
