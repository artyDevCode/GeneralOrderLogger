namespace ApplicationLogger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppList")]
    public partial class AppList
    {
        [Key]
        public int AppID { get; set; }

        [Required]
        [StringLength(50)]
        public string AppCode { get; set; }

        [StringLength(250)]
        public string AppName { get; set; }

        public bool ValidFlag { get; set; }
    }
}
