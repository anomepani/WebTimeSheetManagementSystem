namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ProjectController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class ProjectController : Controller
    {
        /// <summary>
        /// Defines the _IProject
        /// </summary>
        private readonly IProject _IProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        public ProjectController()
        {
            _IProject = new ProjectConcrete();
        }

        // GET: Project
        /// <summary>
        /// The Add
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// The CheckProjectCodeExists
        /// </summary>
        /// <param name="ProjectCode">The ProjectCode<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult CheckProjectCodeExists(string ProjectCode)
        {
            try
            {
                var isProjectCodeExists = false;

                if (ProjectCode != null)
                {
                    isProjectCodeExists = _IProject.CheckProjectCodeExists(ProjectCode);
                }

                if (isProjectCodeExists)
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

        /// <summary>
        /// The CheckProjectNameExists
        /// </summary>
        /// <param name="ProjectName">The ProjectName<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult CheckProjectNameExists(string ProjectName)
        {
            try
            {
                var isProjectNameExists = false;

                if (ProjectName != null)
                {
                    isProjectNameExists = _IProject.CheckProjectNameExists(ProjectName);
                }

                if (isProjectNameExists)
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

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="ProjectMaster">The ProjectMaster<see cref="ProjectMaster"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectMaster ProjectMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _IProject.SaveProject(ProjectMaster);

                if (result > 0)
                {
                    TempData["ProjectMessage"] = "Project Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(ProjectMaster);
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The LoadProjectData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadProjectData()
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

                var projectdata = _IProject.ShowProjects(sortColumn, sortColumnDir, searchValue);
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
        /// The Delete
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult Delete(string ProjectID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ProjectID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var isExistsinTimesheet = _IProject.CheckProjectIDExistsInTimesheet(Convert.ToInt32(ProjectID));

                var isExistsinExpense = _IProject.CheckProjectIDExistsInExpense(Convert.ToInt32(ProjectID));

                if (!isExistsinTimesheet && !isExistsinExpense)
                {
                    var data = _IProject.ProjectDelete(Convert.ToInt32(ProjectID));

                    if (data > 0)
                    {
                        return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                    }
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
