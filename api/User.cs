using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.API
{
    [Table("User")]
    public partial class User
    {
        public int CustomerID { get; set; }

    }
}
