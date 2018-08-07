namespace WebTimeSheetManagement.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="TimeSheetDetailsView" />
    /// </summary>
    public class TimeSheetDetailsView
    {
        /// <summary>
        /// Gets or sets the TimeSheetID
        /// </summary>
        public int TimeSheetID { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek
        /// </summary>
        public string DaysofWeek { get; set; }

        /// <summary>
        /// Gets or sets the Hours
        /// </summary>
        public int? Hours { get; set; }

        /// <summary>
        /// Gets or sets the Period
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName
        /// </summary>
        public string ProjectName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="MainTimeSheetView" />
    /// </summary>
    public class MainTimeSheetView
    {
        /// <summary>
        /// Gets or sets the ListTimeSheetDetails
        /// </summary>
        public List<TimeSheetDetailsView> ListTimeSheetDetails { get; set; }

        /// <summary>
        /// Gets or sets the ListofPeriods
        /// </summary>
        public List<GetPeriods> ListofPeriods { get; set; }

        /// <summary>
        /// Gets or sets the ListofProjectNames
        /// </summary>
        public List<GetProjectNames> ListofProjectNames { get; set; }

        /// <summary>
        /// Gets or sets the ListoDayofWeek
        /// </summary>
        public List<string> ListoDayofWeek { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetMasterID
        /// </summary>
        public int TimeSheetMasterID { get; set; }
    }
}
