namespace WebTimeSheetManagement.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="DisplayViewModel" />
    /// </summary>
    [NotMapped]
    public class DisplayViewModel
    {
        /// <summary>
        /// Gets or sets the ApprovalUser
        /// </summary>
        public int ApprovalUser { get; set; }

        /// <summary>
        /// Gets or sets the SubmittedCount
        /// </summary>
        public int SubmittedCount { get; set; }

        /// <summary>
        /// Gets or sets the ApprovedCount
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// Gets or sets the RejectedCount
        /// </summary>
        public int RejectedCount { get; set; }
    }
}
