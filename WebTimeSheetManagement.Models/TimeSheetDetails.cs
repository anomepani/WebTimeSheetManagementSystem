namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TimeSheetDetails" />
    /// </summary>
    [Table("TimeSheetDetails")]
    public class TimeSheetDetails
    {
        /// <summary>
        /// Gets or sets the TimeSheetID
        /// </summary>
        [Key]
        public int TimeSheetID { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek
        /// </summary>
        public string DaysofWeek { get; set; }

        /// <summary>
        /// Gets or sets the Hours
        /// </summary>
        public int? Hours { get; set; }

        /// <summary>
        /// Gets or sets the Period
        /// </summary>
        public DateTime? Period { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }
    }
}
