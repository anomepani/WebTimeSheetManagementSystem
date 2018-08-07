namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="RegistrationViewSummaryModel" />
    /// </summary>
    [NotMapped]
    public class RegistrationViewSummaryModel
    {
        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        public int RegistrationID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Mobileno
        /// </summary>
        public string Mobileno { get; set; }

        /// <summary>
        /// Gets or sets the EmailID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the AssignToAdmin
        /// </summary>
        public string AssignToAdmin { get; set; }
    }
}
