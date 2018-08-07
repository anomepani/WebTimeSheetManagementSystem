namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="TimeSheetExportConcrete" />
    /// </summary>
    public class TimeSheetExportConcrete : ITimeSheetExport
    {
        /// <summary>
        /// The GetReportofTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetReportofTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetReportofTimeSheet", con)
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
        /// The GetWeekTimeSheetDetails
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetWeekTimeSheetDetails(int TimeSheetMasterID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetWeekTimeSheetDetails", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@TimeSheetMasterID", TimeSheetMasterID);
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
        /// The ListofEmployees
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="List{Registration}"/></returns>
        public List<Registration> ListofEmployees(int UserID)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                var listofemployee = (from registration in _context.Registration
                                      join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                      where AssignedRolesAdmin.AssignToAdmin == UserID
                                      select registration).ToList();

                return listofemployee;
            }
        }

        /// <summary>
        /// The GetTimeSheetMasterIDTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetTimeSheetMasterIDTimeSheet", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
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
        /// The GetTimeSheetMasterIDTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        public DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetTimeSheetbyFromDateandToDateTimeSheet", con)
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

        /// <summary>
        /// The GetUsernamebyRegistrationID
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string GetUsernamebyRegistrationID(int RegistrationID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@RegistrationID", RegistrationID);
                    var result = con.Query<string>("Usp_GetUsernamebyRegistrationID", param, null, true, 0, System.Data.CommandType.StoredProcedure).SingleOrDefault();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
