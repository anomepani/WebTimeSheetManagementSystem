namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="AuditTB" />
    /// </summary>
    [Table("AuditTB")]
    public class AuditTB
    {
        /// <summary>
        /// Gets or sets the AuditID
        /// </summary>
        [Key]
        public int AuditID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the SessionID
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// Gets or sets the IPAddress
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the PageAccessed
        /// </summary>
        public string PageAccessed { get; set; }

        /// <summary>
        /// Gets or sets the LoggedInAt
        /// </summary>
        public Nullable<System.DateTime> LoggedInAt { get; set; }

        /// <summary>
        /// Gets or sets the LoggedOutAt
        /// </summary>
        public Nullable<System.DateTime> LoggedOutAt { get; set; }

        /// <summary>
        /// Gets or sets the LoginStatus
        /// </summary>
        public string LoginStatus { get; set; }

        /// <summary>
        /// Gets or sets the ControllerName
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the ActionName
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the UrlReferrer
        /// </summary>
        public string UrlReferrer { get; set; }
    }
}
