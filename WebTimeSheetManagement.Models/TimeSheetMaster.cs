namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TimeSheetMaster" />
    /// </summary>
    [Table("TimeSheetMaster")]
    public class TimeSheetMaster
    {
        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        [Key]
        public int TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the TotalHours
        /// </summary>
        public int? TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetStatus
        /// </summary>
        public int TimeSheetStatus { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }
    }
}
