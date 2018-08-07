namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="DemoAssignController" />
    /// </summary>
    public class DemoAssignController : Controller
    {
        /// <summary>
        /// Defines the _IAssignRoles
        /// </summary>
        private readonly IAssignRoles _IAssignRoles;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoAssignController"/> class.
        /// </summary>
        public DemoAssignController()
        {
            _IAssignRoles = new AssignRolesConcrete();
        }

        // GET: DemoAssign
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            try
            {
                AssignRolesModel assignRolesModel = new AssignRolesModel
                {
                    ListofAdmins = _IAssignRoles.ListofAdmins(),
                    ListofUser = _IAssignRoles.GetListofUnAssignedUsers()
                };
                return View(assignRolesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="list">The list<see cref="List{UserModel}"/></param>
        /// <param name="assignRolesModel">The assignRolesModel<see cref="AssignRolesModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Index(List<UserModel> list, AssignRolesModel assignRolesModel)
        {
            try
            {
                if (assignRolesModel.ListofUser == null)
                {
                    TempData["MessageErrorRoles"] = "There are no Users to Assign Roles";
                    assignRolesModel.ListofAdmins = _IAssignRoles.ListofAdmins();
                    assignRolesModel.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    return View(assignRolesModel);
                }

                if (ModelState.IsValid)
                {
                    assignRolesModel.CreatedBy = Convert.ToInt32(Session["SuperAdmin"]);
                    _IAssignRoles.SaveAssignedRoles(assignRolesModel);
                    TempData["MessageRoles"] = "Roles Assigned Successfully!";
                }

                assignRolesModel = new AssignRolesModel
                {
                    ListofAdmins = _IAssignRoles.ListofAdmins(),
                    ListofUser = _IAssignRoles.GetListofUnAssignedUsers()
                };

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
