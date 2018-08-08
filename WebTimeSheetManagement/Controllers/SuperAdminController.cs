namespace WebTimeSheetManagement.Controllers
{
    using EventApplicationCore.Library;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Helpers;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="SuperAdminController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class SuperAdminController : Controller
    {
        /// <summary>
        /// Defines the _IRegistration
        /// </summary>
        private readonly IRegistration _IRegistration;

        /// <summary>
        /// Defines the _IRoles
        /// </summary>
        private readonly IRoles _IRoles;

        /// <summary>
        /// Defines the _IAssignRoles
        /// </summary>
        private readonly IAssignRoles _IAssignRoles;

        /// <summary>
        /// Defines the _ICacheManager
        /// </summary>
        private readonly ICacheManager _ICacheManager;

        /// <summary>
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Defines the _IProject
        /// </summary>
        private readonly IProject _IProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperAdminController"/> class.
        /// </summary>
        public SuperAdminController()
        {
            _IRegistration = new RegistrationConcrete();
            _IRoles = new RolesConcrete();
            _IAssignRoles = new AssignRolesConcrete();
            _ICacheManager = new CacheManager();
            _IUsers = new UsersConcrete();
            _IProject = new ProjectConcrete();
        }

        // GET: SuperAdmin
        /// <summary>
        /// The Dashboard
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Dashboard()
        {
            try
            {
                var adminCount = _ICacheManager.Get<object>("AdminCount");

                if (adminCount == null)
                {
                    var admincount = _IUsers.GetTotalAdminsCount();
                    _ICacheManager.Add("AdminCount", admincount);
                    ViewBag.AdminCount = admincount;
                }
                else
                {
                    ViewBag.AdminCount = adminCount;
                }

                var usersCount = _ICacheManager.Get<object>("UsersCount");

                if (usersCount == null)
                {
                    var userscount = _IUsers.GetTotalUsersCount();
                    _ICacheManager.Add("UsersCount", userscount);
                    ViewBag.UsersCount = userscount;
                }
                else
                {
                    ViewBag.UsersCount = usersCount;
                }

                var projectCount = _ICacheManager.Get<object>("ProjectCount");

                if (projectCount == null)
                {
                    var projectcount = _IProject.GetTotalProjectsCounts();
                    _ICacheManager.Add("ProjectCount", projectcount);
                    ViewBag.ProjectCount = projectcount;
                }
                else
                {
                    ViewBag.ProjectCount = projectCount;
                }

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The CreateAdmin
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View(new Registration());
        }

        /// <summary>
        /// The CreateAdmin
        /// </summary>
        /// <param name="registration">The registration<see cref="Registration"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult CreateAdmin(Registration registration)
        {
            try
            {
                var isUsernameExists = _IRegistration.CheckUserNameExists(registration.Username);

                if (isUsernameExists)
                {
                    ModelState.AddModelError(string.Empty, errorMessage: "Username Already Used try unique one!");
                }
                else
                {
                    registration.CreatedOn = DateTime.Now;
                    registration.RoleID = _IRoles.GetRolesofUserbyRolename("Admin");
                    registration.Password = EncryptionLibrary.EncryptText(registration.Password);
                    registration.ConfirmPassword = EncryptionLibrary.EncryptText(registration.ConfirmPassword);
                    if (_IRegistration.AddUser(registration) > 0)
                    {
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        return RedirectToAction("CreateAdmin");
                    }
                    else
                    {
                        return View("CreateAdmin", registration);
                    }
                }

                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// The AssignRoles
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult AssignRoles()
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
        /// The AssignRoles
        /// </summary>
        /// <param name="objassign">The objassign<see cref="AssignRolesModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult AssignRoles(AssignRolesModel objassign)
        {
            try
            {
                if (objassign.ListofUser == null)
                {
                    TempData["MessageErrorRoles"] = "There are no Users to Assign Roles";
                    objassign.ListofAdmins = _IAssignRoles.ListofAdmins();
                    objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    return View(objassign);
                }

                var SelectedCount = (from User in objassign.ListofUser
                                     where User.selectedUsers
                                     select User).Count();

                if (SelectedCount == 0)
                {
                    TempData["MessageErrorRoles"] = "You have not Selected any User to Assign Roles";
                    objassign.ListofAdmins = _IAssignRoles.ListofAdmins();
                    objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    return View(objassign);
                }

                if (ModelState.IsValid)
                {
                    objassign.CreatedBy = Convert.ToInt32(Session["SuperAdmin"]);
                    _IAssignRoles.SaveAssignedRoles(objassign);
                    TempData["MessageRoles"] = "Roles Assigned Successfully!";
                }

                objassign = new AssignRolesModel
                {
                    ListofAdmins = _IAssignRoles.ListofAdmins(),
                    ListofUser = _IAssignRoles.GetListofUnAssignedUsers()
                };

                return RedirectToAction("AssignRoles");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
