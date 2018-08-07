namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ChangePasswordModel" />
    /// </summary>
    [NotMapped]
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the OldPassword
        /// </summary>
        [Required(ErrorMessage = "Enter Old Password")]
        [MinLength(7, ErrorMessage = "Minimum Password must be 7 in charaters")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the NewPassword
        /// </summary>
        [MinLength(7, ErrorMessage = "Minimum Password must be 7 in charaters")]
        [Required(ErrorMessage = "Enter New Password")]
        public string NewPassword { get; set; }
    }
}
