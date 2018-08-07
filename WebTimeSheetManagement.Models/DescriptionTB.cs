namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="DescriptionTB" />
    /// </summary>
    [Table("DescriptionTB")]
    public class DescriptionTB
    {
        /// <summary>
        /// Gets or sets the DescriptionID
        /// </summary>
        [Key]
        public int DescriptionID { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int? TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int? UserID { get; set; }
    }
}
