using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.API
{
    [Table("Customer")]
    public partial class Customer
    {
        public int CustomerID { get; set; }

    }
}