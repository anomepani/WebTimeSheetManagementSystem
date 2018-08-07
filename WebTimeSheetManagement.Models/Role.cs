namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Roles" />
    /// </summary>
    [Table("Roles")]
    public class Roles
    {
        /// <summary>
        /// Gets or sets the RoleID
        /// </summary>
        [Key]
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets the Rolename
        /// </summary>
        public string Rolename { get; set; }
    }
}
