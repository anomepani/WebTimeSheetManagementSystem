namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ProjectMaster" />
    /// </summary>
    [Table("ProjectMaster")]
    public class ProjectMaster
    {
        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        [Key]
        public int ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectCode
        /// </summary>
        [Required(ErrorMessage = "Enter Project Code")]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets the NatureofIndustry
        /// </summary>
        [Required(ErrorMessage = "Enter Nature of Industry")]
        public string NatureofIndustry { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName
        /// </summary>
        [Required(ErrorMessage = "Enter ProjectName")]
        public string ProjectName { get; set; }
    }
}
