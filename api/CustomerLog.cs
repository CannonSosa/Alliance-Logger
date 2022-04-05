using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Logging.API
{
    [Table("CustomerLogs")]
    public partial class CustomerLog
    {
        public int LogID { get; set; }
        public string Application { get; set; }
        public string ApplicationVersion { get; set; }
        public int? CustomerId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string LogContent { get; set; }
    }
}
