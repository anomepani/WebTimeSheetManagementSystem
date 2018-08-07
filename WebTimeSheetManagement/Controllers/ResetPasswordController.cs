namespace WebTimeSheetManagement.Controllers
{
    using EventApplicationCore.Library;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="ResetPasswordController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class ResetPasswordController : Controller
    {
        /// <summary>
        /// Defines the _IRegistration
        /// </summary>
        private readonly IRegistration _IRegistration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordController"/> class.
        /// </summary>
        public ResetPasswordController()
        {
            _IRegistration = new RegistrationConcrete();
        }

        // GET: ResetPassword
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The LoadRegisteredUserData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadRegisteredUserData()
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

                var projectdata = _IRegistration.ListofRegisteredUser(sortColumn, sortColumnDir, searchValue);
                recordsTotal = projectdata.Count();
                var data = projectdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ResetUserPasswordProcess
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult ResetUserPasswordProcess(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(RegistrationID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var Password = EncryptionLibrary.EncryptText("default@123");
                var isPasswordUpdated = _IRegistration.UpdatePassword(RegistrationID, Password);

                if (isPasswordUpdated)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
