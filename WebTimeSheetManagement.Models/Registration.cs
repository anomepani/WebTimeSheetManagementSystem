namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Registration" />
    /// </summary>
    [Table("Registration")]
    public class Registration
    {
        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        [Key]
        public int RegistrationID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Mobileno
        /// </summary>
        [Required(ErrorMessage = "Mobileno Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobileno")]
        public string Mobileno { get; set; }

        /// <summary>
        /// Gets or sets the EmailID
        /// </summary>
        [Required(ErrorMessage = "EmailID Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        [MinLength(6, ErrorMessage = "Minimum Username must be 6 in charaters")]
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [MinLength(7, ErrorMessage = "Minimum Password must be 7 in charaters")]
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmPassword
        /// </summary>
        [Compare("Password", ErrorMessage = "Enter Valid Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the Gender
        /// </summary>
        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the Birthdate
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the DateofJoining
        /// </summary>
        public DateTime? DateofJoining { get; set; }

        /// <summary>
        /// Gets or sets the RoleID
        /// </summary>
        public int? RoleID { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeID
        /// </summary>
        [MaxLength(5, ErrorMessage = "Minimum Password must be 7 in charaters")]
        public string EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the ForceChangePassword
        /// </summary>
        public int? ForceChangePassword { get; set; }
    }
}
