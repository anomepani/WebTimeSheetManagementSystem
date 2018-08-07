namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="TimeSheetExportModel" />
    /// </summary>
    public class TimeSheetExportModel
    {
        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the TotalHours
        /// </summary>
        public int TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="GetPeriods" />
    /// </summary>
    public class GetPeriods
    {
        /// <summary>
        /// Gets or sets the Period
        /// </summary>
        public string Period { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="GetProjectNames" />
    /// </summary>
    public class GetProjectNames
    {
        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName
        /// </summary>
        public string ProjectName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="GetHours" />
    /// </summary>
    public class GetHours
    {
        /// <summary>
        /// Gets or sets the Hours
        /// </summary>
        public int Hours { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="TimeSheetExcelExportModel" />
    /// </summary>
    public class TimeSheetExcelExportModel
    {
        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        [Display(Name = "TimeSheet From Date")]
        [Required(ErrorMessage = "Please Choose From Date")]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        [Display(Name = "TimeSheet To Date")]
        [Required(ErrorMessage = "Please Choose To Date")]
        public DateTime? ToDate { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ExpenseExcelExportModel" />
    /// </summary>
    public class ExpenseExcelExportModel
    {
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
    }

    /// <summary>
    /// Defines the <see cref="TimeSheetExportUserModel" />
    /// </summary>
    public class TimeSheetExportUserModel
    {
        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        [Display(Name = "TimeSheet From Date")]
        [Required(ErrorMessage = "Please Choose From Date")]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        [Display(Name = "TimeSheet To Date")]
        [Required(ErrorMessage = "Please Choose To Date")]
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the RegistrationID
        /// </summary>
        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Please Select Employee Name")]
        public int RegistrationID { get; set; }
    }
}
