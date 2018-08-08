namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Configuration;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Dynamic;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ExpenseConcrete" />
    /// </summary>
    public class ExpenseConcrete : IExpense
    {
        /// <summary>
        /// The AddExpense
        /// </summary>
        /// <param name="ExpenseModel">The ExpenseModel<see cref="ExpenseModel"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddExpense(ExpenseModel ExpenseModel)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    ExpenseModel.HotelBills = ExpenseModel.HotelBills ?? 0;
                    ExpenseModel.TravelBills = ExpenseModel.TravelBills ?? 0;
                    ExpenseModel.MealsBills = ExpenseModel.MealsBills ?? 0;
                    ExpenseModel.LandLineBills = ExpenseModel.LandLineBills ?? 0;
                    ExpenseModel.TransportBills = ExpenseModel.TransportBills ?? 0;
                    ExpenseModel.MobileBills = ExpenseModel.MobileBills ?? 0;
                    ExpenseModel.Miscellaneous = ExpenseModel.Miscellaneous ?? 0;

                    _context.ExpenseModel.Add(ExpenseModel);
                    _context.SaveChanges();
                    int id = ExpenseModel.ExpenseID;
                    return id;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        /// <summary>
        /// The CheckIsDateAlreadyUsed
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckIsDateAlreadyUsed(DateTime? FromDate, DateTime? ToDate, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@FromDate", FromDate);
                    param.Add("@ToDate", ToDate);
                    param.Add("@UserID", UserID);

                    var result = con.Query<int>("Usp_CheckIsDateAlreadyUsed", param, null, false, 0, System.Data.CommandType.StoredProcedure).SingleOrDefault();
                    return result > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ShowExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowExpense(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       where expense.UserID == UserID
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The IsExpenseSubmitted
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsExpenseSubmitted(int ExpenseID, int UserID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from expense in _context.ExpenseModel
                            where expense.ExpenseID == ExpenseID && expense.UserID == UserID
                            select expense).Count();

                return data > 0;
            }
        }

        /// <summary>
        /// The DeleteExpensetByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int DeleteExpensetByExpenseID(int ExpenseID, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ExpenseID", ExpenseID);
                    param.Add("@UserID", UserID);
                    return con.Execute("Usp_DeleteExpenseandDocuments", param, null, 0, System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ShowAllExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowAllExpense(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                       join registration in _context.Registration on expense.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           ProjectName = project.ProjectName,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The ExpenseDetailsbyExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="ExpenseModelView"/></returns>
        public ExpenseModelView ExpenseDetailsbyExpenseID(int ExpenseID)
        {
            try
            {
                using (DatabaseContext _context = new DatabaseContext())
                {
                    var resultExpense = (from expense in _context.ExpenseModel
                                         where expense.ExpenseID == ExpenseID
                                         join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                         select new ExpenseModelView
                                         {
                                             ExpenseID = expense.ExpenseID,
                                             ProjectName = project.ProjectName,
                                             FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                     SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                     SqlFunctions.DateName("year", expense.FromDate),
                                             ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                     SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                     SqlFunctions.DateName("year", expense.ToDate),

                                             CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                     SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                     SqlFunctions.DateName("year", expense.CreatedOn),

                                             ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                             HotelBills = expense.HotelBills,
                                             LandLineBills = expense.LandLineBills,
                                             MealsBills = expense.MealsBills,
                                             Miscellaneous = expense.Miscellaneous,
                                             MobileBills = expense.MobileBills,
                                             PurposeorReason = expense.PurposeorReason,
                                             TotalAmount = expense.TotalAmount,
                                             TransportBills = expense.TransportBills,
                                             TravelBills = expense.TravelBills,
                                             VoucherID = expense.VoucherID,
                                             Comment = expense.Comment
                                         }).FirstOrDefault();

                    return resultExpense;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The UpdateExpenseStatus
        /// </summary>
        /// <param name="ExpenseApprovalModel">The ExpenseApprovalModel<see cref="ExpenseApprovalModel"/></param>
        /// <param name="ExpenseStatus">The ExpenseStatus<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdateExpenseStatus(ExpenseApprovalModel ExpenseApprovalModel, int ExpenseStatus)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@ExpenseID", ExpenseApprovalModel.ExpenseID);
                    param.Add("@Comment", ExpenseApprovalModel.Comment);
                    param.Add("@ExpenseStatus", ExpenseStatus);
                    var result = con.Execute("Usp_UpdateExpenseStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// The InsertExpenseAuditLog
        /// </summary>
        /// <param name="expenseaudittb">The expenseaudittb<see cref="ExpenseAuditTB"/></param>
        public void InsertExpenseAuditLog(ExpenseAuditTB expenseaudittb)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.ExpenseAuditTB.Add(expenseaudittb);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetExpenseAuditCountByAdminID
        /// </summary>
        /// <param name="AdminID">The AdminID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        public DisplayViewModel GetExpenseAuditCountByAdminID(string AdminID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@AdminID", AdminID);
                return con.Query<DisplayViewModel>("Usp_GetExpenseAuditCountByAdminID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// The ShowAllSubmittedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowAllSubmittedExpense(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       join expenseaudittb in _context.ExpenseAuditTB on expense.ExpenseID equals expenseaudittb.ExpenseID
                                       join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                       join registration in _context.Registration on expense.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && expenseaudittb.Status == 1
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           ProjectName = project.ProjectName,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The ShowAllApprovedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowAllApprovedExpense(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       join expenseaudittb in _context.ExpenseAuditTB on expense.ExpenseID equals expenseaudittb.ExpenseID
                                       join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                       join registration in _context.Registration on expense.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && expenseaudittb.Status == 2
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           ProjectName = project.ProjectName,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The ShowAllRejectedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowAllRejectedExpense(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       join expenseaudittb in _context.ExpenseAuditTB on expense.ExpenseID equals expenseaudittb.ExpenseID
                                       join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                       join registration in _context.Registration on expense.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && expenseaudittb.Status == 3
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           ProjectName = project.ProjectName,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The GetExpenseAuditCountByUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        public DisplayViewModel GetExpenseAuditCountByUserID(string UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                return con.Query<DisplayViewModel>("Usp_GetExpenseAuditCountByUserID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// The UpdateExpenseAuditStatus
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="Comment">The Comment<see cref="string"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdateExpenseAuditStatus(int ExpenseID, string Comment, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@ExpenseID", ExpenseID);
                    param.Add("@Comment", Comment);
                    param.Add("@Status", Status);
                    var result = con.Execute("Usp_ChangeExpenseStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// The IsExpenseALreadyProcessed
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsExpenseALreadyProcessed(int ExpenseID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from timesheet in _context.ExpenseAuditTB
                            where timesheet.ExpenseID == ExpenseID && timesheet.Status != 1
                            select timesheet).Count();

                return data > 0;
            }
        }

        /// <summary>
        /// The ShowExpenseStatus
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="ExpenseStatus">The ExpenseStatus<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        public IQueryable<ExpenseModelView> ShowExpenseStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int ExpenseStatus)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from expense in _context.ExpenseModel
                                       join expenseaudittb in _context.ExpenseAuditTB on expense.ExpenseID equals expenseaudittb.ExpenseID
                                       join project in _context.ProjectMaster on expense.ProjectID equals project.ProjectID
                                       join registration in _context.Registration on expense.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where expense.UserID == UserID && expenseaudittb.Status == ExpenseStatus
                                       select new ExpenseModelView
                                       {
                                           ExpenseID = expense.ExpenseID,
                                           ProjectName = project.ProjectName,
                                           FromDate = SqlFunctions.DateName("day", expense.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.FromDate),
                                           ToDate = SqlFunctions.DateName("day", expense.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", expense.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)expense.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", expense.CreatedOn),

                                           ExpenseStatus = expense.ExpenseStatus == 1 ? "Submitted" : expense.ExpenseStatus == 2 ? "Approved" : "Rejected",
                                           HotelBills = expense.HotelBills,
                                           LandLineBills = expense.LandLineBills,
                                           MealsBills = expense.MealsBills,
                                           Miscellaneous = expense.Miscellaneous,
                                           MobileBills = expense.MobileBills,
                                           PurposeorReason = expense.PurposeorReason,
                                           TotalAmount = expense.TotalAmount,
                                           TransportBills = expense.TransportBills,
                                           TravelBills = expense.TravelBills,
                                           VoucherID = expense.VoucherID,
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }
    }
}
