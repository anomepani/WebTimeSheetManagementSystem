namespace WebTimeSheetManagement.Interface
{
    using System;
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IExpense" />
    /// </summary>
    public interface IExpense
    {
        /// <summary>
        /// The AddExpense
        /// </summary>
        /// <param name="ExpenseModel">The ExpenseModel<see cref="ExpenseModel"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddExpense(ExpenseModel ExpenseModel);

        /// <summary>
        /// The CheckIsDateAlreadyUsed
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckIsDateAlreadyUsed(DateTime? FromDate, DateTime? ToDate, int UserID);

        /// <summary>
        /// The ShowExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowExpense(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The IsExpenseSubmitted
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool IsExpenseSubmitted(int ExpenseID, int UserID);

        /// <summary>
        /// The DeleteExpensetByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int DeleteExpensetByExpenseID(int ExpenseID, int UserID);

        /// <summary>
        /// The ShowAllExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowAllExpense(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The ExpenseDetailsbyExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="ExpenseModelView"/></returns>
        ExpenseModelView ExpenseDetailsbyExpenseID(int ExpenseID);

        /// <summary>
        /// The UpdateExpenseStatus
        /// </summary>
        /// <param name="ExpenseApprovalModel">The ExpenseApprovalModel<see cref="ExpenseApprovalModel"/></param>
        /// <param name="ExpenseStatus">The ExpenseStatus<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdateExpenseStatus(ExpenseApprovalModel ExpenseApprovalModel, int ExpenseStatus);

        /// <summary>
        /// The InsertExpenseAuditLog
        /// </summary>
        /// <param name="expenseaudittb">The expenseaudittb<see cref="ExpenseAuditTB"/></param>
        void InsertExpenseAuditLog(ExpenseAuditTB expenseaudittb);

        /// <summary>
        /// The GetExpenseAuditCountByAdminID
        /// </summary>
        /// <param name="AdminID">The AdminID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        DisplayViewModel GetExpenseAuditCountByAdminID(string AdminID);

        /// <summary>
        /// The ShowAllSubmittedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowAllSubmittedExpense(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The ShowAllRejectedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowAllRejectedExpense(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The ShowAllApprovedExpense
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowAllApprovedExpense(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The GetExpenseAuditCountByUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        DisplayViewModel GetExpenseAuditCountByUserID(string UserID);

        /// <summary>
        /// The UpdateExpenseAuditStatus
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="Comment">The Comment<see cref="string"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdateExpenseAuditStatus(int ExpenseID, string Comment, int Status);

        /// <summary>
        /// The IsExpenseALreadyProcessed
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool IsExpenseALreadyProcessed(int ExpenseID);

        /// <summary>
        /// The ShowExpenseStatus
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="ExpenseStatus">The ExpenseStatus<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{ExpenseModelView}"/></returns>
        IQueryable<ExpenseModelView> ShowExpenseStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int ExpenseStatus);
    }
}
