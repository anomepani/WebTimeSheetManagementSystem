namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TimeSheetAuditTB" />
    /// </summary>
    [Table("TimeSheetAuditTB")]
    public class TimeSheetAuditTB
    {
        /// <summary>
        /// Gets or sets the ApprovalTimeSheetLogID
        /// </summary>
        [Key]
        public int ApprovalTimeSheetLogID { get; set; }

        /// <summary>
        /// Gets or sets the ApprovalUser
        /// </summary>
        public int ApprovalUser { get; set; }

        /// <summary>
        /// Gets or sets the ProcessedDate
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetID
        /// </summary>
        public int TimeSheetID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ExpenseAuditTB" />
    /// </summary>
    [Table("ExpenseAuditTB")]
    public class ExpenseAuditTB
    {
        /// <summary>
        /// Gets or sets the ApprovaExpenselLogID
        /// </summary>
        [Key]
        public int ApprovaExpenselLogID { get; set; }

        /// <summary>
        /// Gets or sets the ApprovalUser
        /// </summary>
        public int ApprovalUser { get; set; }

        /// <summary>
        /// Gets or sets the ProcessedDate
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseID
        /// </summary>
        public int ExpenseID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }
    }
}
