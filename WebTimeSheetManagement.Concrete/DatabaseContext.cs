namespace WebTimeSheetManagement.Concrete
{
    using System.Data.Entity;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="DatabaseContext" />
    /// </summary>
    public partial class DatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        public DatabaseContext()
            : base("name=TimesheetDBEntities")
        {
        }

        /// <summary>
        /// Gets or sets the Registration
        /// </summary>
        public DbSet<Registration> Registration { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public DbSet<Roles> Role { get; set; }

        /// <summary>
        /// Gets or sets the ProjectMaster
        /// </summary>
        public DbSet<ProjectMaster> ProjectMaster { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetMaster
        /// </summary>
        public DbSet<TimeSheetMaster> TimeSheetMaster { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetDetails
        /// </summary>
        public DbSet<TimeSheetDetails> TimeSheetDetails { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseModel
        /// </summary>
        public DbSet<ExpenseModel> ExpenseModel { get; set; }

        /// <summary>
        /// Gets or sets the Documents
        /// </summary>
        public DbSet<Documents> Documents { get; set; }

        /// <summary>
        /// Gets or sets the TimeSheetAuditTB
        /// </summary>
        public DbSet<TimeSheetAuditTB> TimeSheetAuditTB { get; set; }

        /// <summary>
        /// Gets or sets the ExpenseAuditTB
        /// </summary>
        public DbSet<ExpenseAuditTB> ExpenseAuditTB { get; set; }

        /// <summary>
        /// Gets or sets the AuditTB
        /// </summary>
        public DbSet<AuditTB> AuditTB { get; set; }

        /// <summary>
        /// Gets or sets the DescriptionTB
        /// </summary>
        public DbSet<DescriptionTB> DescriptionTB { get; set; }

        /// <summary>
        /// Gets or sets the AssignedRoles
        /// </summary>
        public DbSet<AssignedRoles> AssignedRoles { get; set; }

        /// <summary>
        /// Gets or sets the NotificationsTBs
        /// </summary>
        public System.Data.Entity.DbSet<WebTimeSheetManagement.Models.NotificationsTB> NotificationsTBs { get; set; }
    }
}
