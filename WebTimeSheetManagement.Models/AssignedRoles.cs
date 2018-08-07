namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="AssignedRoles" />
    /// </summary>
    [Table("AssignedRoles")]
    public class AssignedRoles
    {
        /// <summary>
        /// Gets or sets the AssignedRolesID
        /// </summary>
        [Key]
        public int AssignedRolesID { get; set; }

        /// <summary>
        /// Gets or sets the AssignToAdmin
        /// </summary>
        public int? AssignToAdmin { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        public int RegistrationID { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }
    }
}
