namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="LoginViewModel" />
    /// </summary>
    [NotMapped]
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
