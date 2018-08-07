namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="AllExpenseController" />
    /// </summary>
    [ValidateUserSession]
    public class AllExpenseController : Controller
    {
        /// <summary>
        /// Defines the _IExpense
        /// </summary>
        private readonly IExpense _IExpense;

        /// <summary>
        /// Defines the _IDocument
        /// </summary>
        private readonly IDocument _IDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllExpenseController"/> class.
        /// </summary>
        public AllExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IDocument = new DocumentConcrete();
        }

        // GET: AllExpense
        /// <summary>
        /// The Expense
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Expense()
        {
            return View();
        }

        /// <summary>
        /// The LoadExpenseData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadExpenseData()
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

                var v = _IExpense.ShowExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

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
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult Delete(int ExpenseID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ExpenseID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var dataSubmitted = _IExpense.IsExpenseSubmitted(ExpenseID, Convert.ToInt32(Session["UserID"]));

                if (dataSubmitted)
                {
                    var data = _IExpense.DeleteExpensetByExpenseID(ExpenseID, Convert.ToInt32(Session["UserID"]));

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
                    return Json(data: "Cannot", behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Details(int ExpenseID)
        {
            try
            {
                var ExpenseDetails = _IExpense.ExpenseDetailsbyExpenseID(ExpenseID);
                ViewBag.documents = _IDocument.GetListofDocumentByExpenseID(ExpenseID);
                return PartialView("_Details", ExpenseDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Download
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="string"/></param>
        /// <param name="DocumentID">The DocumentID<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Download(string ExpenseID, int DocumentID)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ExpenseID)) && !string.IsNullOrEmpty(Convert.ToString(DocumentID)))
                {
                    var document = _IDocument.GetDocumentByExpenseID(Convert.ToInt32(ExpenseID), Convert.ToInt32(DocumentID));
                    return File(document.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
                }
                else
                {
                    return RedirectToAction("Expense", "ShowAllExpense");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The SubmittedExpense
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult SubmittedExpense()
        {
            return View();
        }

        /// <summary>
        /// The ApprovedExpense
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult ApprovedExpense()
        {
            return View();
        }

        /// <summary>
        /// The RejectedExpense
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult RejectedExpense()
        {
            return View();
        }

        /// <summary>
        /// The LoadSubmittedExpenseData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadSubmittedExpenseData()
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

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadApprovedExpenseData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadApprovedExpenseData()
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

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadRejectedExpenseData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadRejectedExpenseData()
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

                var v = _IExpense.ShowExpenseStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
