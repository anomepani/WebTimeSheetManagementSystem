namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="ErrorController" />
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        /// <summary>
        /// The Error
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Error()
        {
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

            return View("Error");
        }
    }
}
