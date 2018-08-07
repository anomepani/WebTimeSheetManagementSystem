namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="NotificationsTB" />
    /// </summary>
    [Table("NotificationsTB")]
    public class NotificationsTB
    {
        /// <summary>
        /// Gets or sets the NotificationsID
        /// </summary>
        [Key]
        public int NotificationsID { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        [Required(ErrorMessage = "FromDate Required")]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        [Required(ErrorMessage = "ToDate Required")]
        public DateTime? ToDate { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="NotificationsTB_ViewModel" />
    /// </summary>
    [NotMapped]
    public class NotificationsTB_ViewModel
    {
        /// <summary>
        /// Gets or sets the NotificationsID
        /// </summary>
        [Key]
        public int NotificationsID { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// Gets or sets the Min
        /// </summary>
        public int? Min { get; set; }
    }
}
