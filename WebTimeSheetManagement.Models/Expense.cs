namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ExpenseModel" />
    /// </summary>
    [Table("Expense")]
    public class ExpenseModel
    {
        /// <summary>
        /// Gets or sets the ExpenseID
        /// </summary>
        [Key]
        public int ExpenseID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        [Display(Name = "Project")]
        [Required(ErrorMessage = "Choose Project")]
        public int? ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the PurposeorReason
        /// </summary>
        [Display(Name = "Purpose / Reason")]
        [Required(ErrorMessage = "Please Enter Purpose/Reason")]
        public string PurposeorReason { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseStatus
        /// </summary>
        public int ExpenseStatus { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        [Display(Name = "Expense From Date")]
        [Required(ErrorMessage = "Please Choose From Date")]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        [Display(Name = "Expense To Date")]
        [Required(ErrorMessage = "Please Choose To Date")]
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the VoucherID
        /// </summary>
        [Display(Name = "Expense ID / Voucher ID")]
        public string VoucherID { get; set; }

        /// <summary>
        /// Gets or sets the HotelBills
        /// </summary>
        [Display(Name = "Hotel")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? HotelBills { get; set; }

        /// <summary>
        /// Gets or sets the TravelBills
        /// </summary>
        [Display(Name = "Travel")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? TravelBills { get; set; }

        /// <summary>
        /// Gets or sets the MealsBills
        /// </summary>
        [Display(Name = "Meals")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? MealsBills { get; set; }

        /// <summary>
        /// Gets or sets the LandLineBills
        /// </summary>
        [Display(Name = "Land Line bills")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? LandLineBills { get; set; }

        /// <summary>
        /// Gets or sets the TransportBills
        /// </summary>
        [Display(Name = "Transport expense")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? TransportBills { get; set; }

        /// <summary>
        /// Gets or sets the MobileBills
        /// </summary>
        [Display(Name = "Mobile bills")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? MobileBills { get; set; }

        /// <summary>
        /// Gets or sets the Miscellaneous
        /// </summary>
        [Display(Name = "Miscellaneous expense")]
        public int? Miscellaneous { get; set; }

        /// <summary>
        /// Gets or sets the TotalAmount
        /// </summary>
        [Display(Name = "Total Amount")]
        public int? TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the FileName
        /// </summary>
        [NotMapped]
        [Display(Name = "Attachments")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }
    }
}
