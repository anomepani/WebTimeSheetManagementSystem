namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="DocumentsVM" />
    /// </summary>
    [NotMapped]
    public class DocumentsVM
    {
        /// <summary>
        /// Gets or sets the DocumentID
        /// </summary>
        public int DocumentID { get; set; }

        /// <summary>
        /// Gets or sets the DocumentName
        /// </summary>
        public string DocumentName { get; set; }

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
