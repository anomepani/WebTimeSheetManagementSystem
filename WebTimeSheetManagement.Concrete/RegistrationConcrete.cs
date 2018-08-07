namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Dynamic;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="RegistrationConcrete" />
    /// </summary>
    public class RegistrationConcrete : IRegistration
    {
        /// <summary>
        /// The CheckUserNameExists
        /// </summary>
        /// <param name="Username">The Username<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckUserNameExists(string Username)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.Registration
                                  where user.Username == Username
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="entity">The entity<see cref="Registration"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddUser(Registration entity)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.Registration.Add(entity);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ListofRegisteredUser
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{Registration}"/></returns>
        public IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search)
        {
            try
            {
                var _context = new DatabaseContext();

                var IQueryableRegistered = (from register in _context.Registration
                                            select register
                                );

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    IQueryableRegistered = IQueryableRegistered.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    IQueryableRegistered = IQueryableRegistered.Where(m => m.Username.Contains(Search) || m.Name.Contains(Search));
                }

                return IQueryableRegistered;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The UpdatePassword
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="string"/></param>
        /// <param name="Password">The Password<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdatePassword(string RegistrationID, string Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@RegistrationID", RegistrationID);
                    param.Add("@Password", Password);
                    var result = con.Execute("Usp_UpdatePasswordbyRegistrationID", param, null, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
