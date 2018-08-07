namespace WebTimeSheetManagement.Controllers
{
    using CaptchaMvc.HtmlHelpers;
    using EventApplicationCore.Library;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Helpers;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="LoginController" />
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Defines the _ILogin
        /// </summary>
        private readonly ILogin _ILogin;

        /// <summary>
        /// Defines the _IAssignRoles
        /// </summary>
        private readonly IAssignRoles _IAssignRoles;

        /// <summary>
        /// Defines the _ICacheManager
        /// </summary>
        private readonly ICacheManager _ICacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController()
        {
            _ILogin = new LoginConcrete();
            _IAssignRoles = new AssignRolesConcrete();
            _ICacheManager = new CacheManager();
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="loginViewModel">The loginViewModel<see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ViewBag.errormessage = "Error: captcha entered is not valid.";

                    return View(loginViewModel);
                }

                if (!string.IsNullOrEmpty(loginViewModel.Username) && !string.IsNullOrEmpty(loginViewModel.Password))
                {
                    var Username = loginViewModel.Username;
                    var password = EncryptionLibrary.EncryptText(loginViewModel.Password);

                    var result = _ILogin.ValidateUser(Username, password);

                    if (result != null)
                    {
                        if (result.RegistrationID == 0 || result.RegistrationID < 0)
                        {
                            ViewBag.errormessage = "Entered Invalid Username and Password";
                        }
                        else
                        {
                            var RoleID = result.RoleID;
                            Remove_Anonymous_Cookies(); //Remove Anonymous_Cookies

                            Session["RoleID"] = Convert.ToString(result.RoleID);
                            Session["Username"] = Convert.ToString(result.Username);
                            if (RoleID == 1)
                            {
                                Session["AdminUser"] = Convert.ToString(result.RegistrationID);

                                if (result.ForceChangePassword == 1)
                                {
                                    return RedirectToAction("ChangePassword", "UserProfile");
                                }

                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (RoleID == 2)
                            {
                                if (!_IAssignRoles.CheckIsUserAssignedRole(result.RegistrationID))
                                {
                                    ViewBag.errormessage = "Approval Pending";
                                    return View(loginViewModel);
                                }

                                Session["UserID"] = Convert.ToString(result.RegistrationID);

                                if (result.ForceChangePassword == 1)
                                {
                                    return RedirectToAction("ChangePassword", "UserProfile");
                                }

                                return RedirectToAction("Dashboard", "User");
                            }
                            else if (RoleID == 3)
                            {
                                Session["SuperAdmin"] = Convert.ToString(result.RegistrationID);
                                return RedirectToAction("Dashboard", "SuperAdmin");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.errormessage = "Entered Invalid Username and Password";
                        return View(loginViewModel);
                    }
                }
                return View(loginViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Logout
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SuperAdmin"])))
                {
                    _ICacheManager.Clear("AdminCount");
                    _ICacheManager.Clear("UsersCount");
                    _ICacheManager.Clear("ProjectCount");
                }

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();

                HttpCookie Cookies = new HttpCookie("WebTime")
                {
                    Value = "",
                    Expires = DateTime.Now.AddHours(-1)
                };
                Response.Cookies.Add(Cookies);
                HttpContext.Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Remove_Anonymous_Cookies
        /// </summary>
        [NonAction]
        public void Remove_Anonymous_Cookies()
        {
            try
            {
                if (Request.Cookies["WebTime"] != null)
                {
                    var option = new HttpCookie("WebTime")
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    };
                    Response.Cookies.Add(option);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
