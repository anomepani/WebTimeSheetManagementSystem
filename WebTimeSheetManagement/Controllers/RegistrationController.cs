namespace WebTimeSheetManagement.Controllers
{
    using EventApplicationCore.Library;
    using System;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="RegistrationController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class RegistrationController : Controller
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
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController()
        {
            _IRegistration = new RegistrationConcrete();
            _IRoles = new RolesConcrete();
        }

        // GET: Registration/Create
        /// <summary>
        /// The Registration
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Registration()
        {
            return View(new Registration());
        }

        // POST: Registration/Create
        /// <summary>
        /// The Registration
        /// </summary>
        /// <param name="registration">The registration<see cref="Registration"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registration registration)
        {
            try
            {
                var isUsernameExists = _IRegistration.CheckUserNameExists(registration.Username);

                if (isUsernameExists)
                {
                    ModelState.AddModelError("", errorMessage: "Username Already Used try unique one!");
                }
                else
                {
                    registration.CreatedOn = DateTime.Now;
                    registration.RoleID = _IRoles.GetRolesofUserbyRolename("Users");
                    registration.Password = EncryptionLibrary.EncryptText(registration.Password);
                    registration.ConfirmPassword = EncryptionLibrary.EncryptText(registration.ConfirmPassword);
                    if (_IRegistration.AddUser(registration) > 0)
                    {
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        return RedirectToAction("Registration");
                    }
                    else
                    {
                        return View(registration);
                    }
                }
                return RedirectToAction("Registration");
            }
            catch
            {
                return View(registration);
            }
        }

        /// <summary>
        /// The CheckUserNameExists
        /// </summary>
        /// <param name="Username">The Username<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult CheckUserNameExists(string Username)
        {
            try
            {
                var isUsernameExists = false;

                if (Username != null)
                {
                    isUsernameExists = _IRegistration.CheckUserNameExists(Username);
                }

                if (isUsernameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
