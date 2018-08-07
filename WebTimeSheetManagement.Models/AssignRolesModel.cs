namespace WebTimeSheetManagement.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="AssignRolesModel" />
    /// </summary>
    [NotMapped]
    public class AssignRolesModel
    {
        /// <summary>
        /// Gets or sets the ListofAdmins
        /// </summary>
        public List<AdminModel> ListofAdmins { get; set; }

        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        [Required(ErrorMessage = "Choose Admin")]
        public int RegistrationID { get; set; }

        /// <summary>
        /// Gets or sets the ListofUser
        /// </summary>
        public List<UserModel> ListofUser { get; set; }

        /// <summary>
        /// Gets or sets the AssignToAdmin
        /// </summary>
        public int? AssignToAdmin { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int? CreatedBy { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="AdminModel" />
    /// </summary>
    [NotMapped]
    public class AdminModel
    {
        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        public string RegistrationID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="UserModel" />
    /// </summary>
    [NotMapped]
    public class UserModel
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
        /// Gets or sets a value indicating whether selectedUsers
        /// </summary>
        public bool selectedUsers { get; set; }

        /// <summary>
        /// Gets or sets the AssignToAdmin
        /// </summary>
        public string AssignToAdmin { get; set; }
    }
}
