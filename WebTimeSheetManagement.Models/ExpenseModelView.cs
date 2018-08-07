namespace WebTimeSheetManagement.Models
{
    /// <summary>
    /// Defines the <see cref="ExpenseModelView" />
    /// </summary>
    public class ExpenseModelView
    {
        /// <summary>
        /// Gets or sets the ExpenseID
        /// </summary>
        public int ExpenseID { get; set; }

        /// <summary>
        /// Gets or sets the PurposeorReason
        /// </summary>
        public string PurposeorReason { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseStatus
        /// </summary>
        public string ExpenseStatus { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the VoucherID
        /// </summary>
        public string VoucherID { get; set; }

        /// <summary>
        /// Gets or sets the HotelBills
        /// </summary>
        public int? HotelBills { get; set; }

        /// <summary>
        /// Gets or sets the TravelBills
        /// </summary>
        public int? TravelBills { get; set; }

        /// <summary>
        /// Gets or sets the MealsBills
        /// </summary>
        public int? MealsBills { get; set; }

        /// <summary>
        /// Gets or sets the LandLineBills
        /// </summary>
        public int? LandLineBills { get; set; }

        /// <summary>
        /// Gets or sets the TransportBills
        /// </summary>
        public int? TransportBills { get; set; }

        /// <summary>
        /// Gets or sets the MobileBills
        /// </summary>
        public int? MobileBills { get; set; }

        /// <summary>
        /// Gets or sets the Miscellaneous
        /// </summary>
        public int? Miscellaneous { get; set; }

        /// <summary>
        /// Gets or sets the TotalAmount
        /// </summary>
        public int? TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName
        /// </summary>
        public string ProjectName { get; set; }
    }
}
