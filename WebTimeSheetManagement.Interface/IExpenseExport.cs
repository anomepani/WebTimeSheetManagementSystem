namespace WebTimeSheetManagement.Interface
{
    using System;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="IExpenseExport" />
    /// </summary>
    public interface IExpenseExport
    {
        /// <summary>
        /// The GetReportofExpense
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetReportofExpense(DateTime? FromDate, DateTime? ToDate, int UserID);

        /// <summary>
        /// The GetAllReportofExpense
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetAllReportofExpense(DateTime? FromDate, DateTime? ToDate);
    }
}
