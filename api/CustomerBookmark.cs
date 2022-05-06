using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Logging.API
{
    [Table("CustomerBookmark")]
    public partial class CustomerBookmark
    {
        public int BookmarkID { get; set; }
        public int CustomerID { get; set; }
        public int LogID { get; set; }

    }
}