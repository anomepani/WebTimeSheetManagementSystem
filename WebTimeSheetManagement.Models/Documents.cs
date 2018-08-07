namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Documents" />
    /// </summary>
    public class Documents
    {
        /// <summary>
        /// Gets or sets the DocumentID
        /// </summary>
        [Key]
        public int DocumentID { get; set; }

        /// <summary>
        /// Gets or sets the DocumentName
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Gets or sets the DocumentBytes
        /// </summary>
        public byte[] DocumentBytes { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseID
        /// </summary>
        public int ExpenseID { get; set; }

        /// <summary>
        /// Gets or sets the DocumentType
        /// </summary>
        public string DocumentType { get; set; }
    }
}
