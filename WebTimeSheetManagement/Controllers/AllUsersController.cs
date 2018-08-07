namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="AllUsersController" />
    /// </summary>
    [ValidateSuperAdminSession]

    public class AllUsersController : Controller
    {
        /// <summary>
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllUsersController"/> class.
        /// </summary>
        public AllUsersController()
        {
            _IUsers = new UsersConcrete();
        }

        // GET: AllUsers
        /// <summary>
        /// The Users
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Users()
        {
            return View();
        }

        /// <summary>
        /// The LoadUsersData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadUsersData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IUsers.ShowallUsers(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The UserDetails
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int?"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult UserDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetUserDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Admin
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Admin()
        {
            return View();
        }

        /// <summary>
        /// The LoadAdminsData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadAdminsData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IUsers.ShowallAdmin(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The AdminDetails
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int?"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult AdminDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetAdminDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
