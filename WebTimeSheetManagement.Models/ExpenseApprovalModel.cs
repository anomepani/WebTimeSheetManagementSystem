namespace WebTimeSheetManagement.Models
{
    /// <summary>
    /// Defines the <see cref="ExpenseApprovalModel" />
    /// </summary>
    public class ExpenseApprovalModel
    {
        /// <summary>
        /// Gets or sets the ExpenseID
        /// </summary>
        public int ExpenseID { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="TimeSheetApproval" />
    /// </summary>
    public class TimeSheetApproval
    {
        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }
    }
}
