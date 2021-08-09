namespace ApplicationLogger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppLog")]
    public partial class AppLog
    {
        public int AppLogID { get; set; }

        public DateTime LogTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Workstation { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string AdditionalUserInfo { get; set; }

        public int AppID { get; set; }

        [Required]
        public string LogDetail { get; set; }

        public string LogDetail_Additional { get; set; }
    }
}
