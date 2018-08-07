namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="LoginConcrete" />
    /// </summary>
    public class LoginConcrete : ILogin
    {
        /// <summary>
        /// The ValidateUser
        /// </summary>
        /// <param name="userName">The userName<see cref="string"/></param>
        /// <param name="passWord">The passWord<see cref="string"/></param>
        /// <returns>The <see cref="Registration"/></returns>
        public Registration ValidateUser(string userName, string passWord)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var validate = (from user in _context.Registration
                                    where user.Username == userName && user.Password == passWord
                                    select user).SingleOrDefault();

                    return validate;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The UpdatePassword
        /// </summary>
        /// <param name="NewPassword">The NewPassword<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdatePassword(string NewPassword, int UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@NewPassword", NewPassword);
                    param.Add("@UserID", UserID);
                    var result = con.Execute("Usp_Updatepassword", param, sql, 0, System.Data.CommandType.StoredProcedure);
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
        /// The GetPasswordbyUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string GetPasswordbyUserID(int UserID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var password = (from temppassword in _context.Registration
                                    where temppassword.RegistrationID == UserID
                                    select temppassword.Password).FirstOrDefault();

                    return password;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
