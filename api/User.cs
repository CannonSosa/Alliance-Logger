using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.API
{
    [Table("Users")]
    public partial class User : IdentityUser<int>
    {         
    }
}
