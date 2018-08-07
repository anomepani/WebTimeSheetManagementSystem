namespace WebTimeSheetManagement.Concrete
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="ExpenseExportConcrete" />
    /// </summary>
    public class ExpenseExportConcrete : IExpenseExport
    {
        /// <summary>
        /// The GetReportofExpense
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetReportofExpense(DateTime? FromDate, DateTime? ToDate, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetReportofExpense", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@AssignTo", UserID);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetAllReportofExpense
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetAllReportofExpense(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetAllReportofExpense", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    SqlDataAdapter da = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
