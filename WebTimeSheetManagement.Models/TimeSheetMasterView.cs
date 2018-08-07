namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TimeSheetMasterView" />
    /// </summary>
    [NotMapped]
    public class TimeSheetMasterView
    {
        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        public string ToDate { get; set; }

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
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the SubmittedMonth
        /// </summary>
        public string SubmittedMonth { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetStatus
        /// </summary>
        public string TimeSheetStatus { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }
    }
}
