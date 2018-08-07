namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="AllRolesController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class AllRolesController : Controller
    {
        /// <summary>
        /// Defines the _IAssignRoles
        /// </summary>
        private readonly IAssignRoles _IAssignRoles;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllRolesController"/> class.
        /// </summary>
        public AllRolesController()
        {
            _IAssignRoles = new AssignRolesConcrete();
        }

        // GET: AllRoles
        /// <summary>
        /// The Roles
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Roles()
        {
            return View();
        }

        /// <summary>
        /// The LoadRolesData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadRolesData()
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

                var rolesData = _IAssignRoles.ShowallRoles(sortColumn, sortColumnDir, searchValue);
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
        /// The RemovefromRole
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult RemovefromRole(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(RegistrationID))
                {
                    return RedirectToAction("Roles");
                }

                var role = _IAssignRoles.RemovefromUserRole(RegistrationID);
                return Json(role);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
