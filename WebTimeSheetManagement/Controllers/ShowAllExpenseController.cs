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
    /// Defines the <see cref="ShowAllExpenseController" />
    /// </summary>
    [ValidateAdminSession]
    public class ShowAllExpenseController : Controller
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
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowAllExpenseController"/> class.
        /// </summary>
        public ShowAllExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IDocument = new DocumentConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: ShowAllExpense
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

                var expensedata = _IExpense.ShowAllExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Expense", "ShowAllExpense");
                }
                var data = _IExpense.ExpenseDetailsbyExpenseID(Convert.ToInt32(id));
                ViewBag.documents = _IDocument.GetListofDocumentByExpenseID(Convert.ToInt32(id));
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Approval
        /// </summary>
        /// <param name="expenseapprovalmodel">The expenseapprovalmodel<see cref="ExpenseApprovalModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Approval(ExpenseApprovalModel expenseapprovalmodel)
        {
            try
            {
                if (expenseapprovalmodel.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(expenseapprovalmodel.ExpenseID)))
                {
                    return Json(false);
                }

                _IExpense.UpdateExpenseStatus(expenseapprovalmodel, 2); // Approve

                if (_IExpense.IsExpenseALreadyProcessed(expenseapprovalmodel.ExpenseID))
                {
                    _IExpense.UpdateExpenseAuditStatus(expenseapprovalmodel.ExpenseID, expenseapprovalmodel.Comment, 2);
                }
                else
                {
                    _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(expenseapprovalmodel, 2));
                }

                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Rejected
        /// </summary>
        /// <param name="expenseapprovalmodel">The expenseapprovalmodel<see cref="ExpenseApprovalModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Rejected(ExpenseApprovalModel expenseapprovalmodel)
        {
            try
            {
                if (expenseapprovalmodel.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(expenseapprovalmodel.ExpenseID)))
                {
                    return Json(false);
                }

                _IExpense.UpdateExpenseStatus(expenseapprovalmodel, 3); // Reject

                if (_IExpense.IsExpenseALreadyProcessed(expenseapprovalmodel.ExpenseID))
                {
                    _IExpense.UpdateExpenseAuditStatus(expenseapprovalmodel.ExpenseID, expenseapprovalmodel.Comment, 3);
                }
                else
                {
                    _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(expenseapprovalmodel, 3));
                }

                return Json(true);
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

                var dataSubmitted = _IExpense.IsExpenseSubmitted(ExpenseID, Convert.ToInt32(Session["AdminUser"]));

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
        /// The Download
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="DocumentID">The DocumentID<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Download(string id, int DocumentID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(id)) && !string.IsNullOrEmpty(Convert.ToString(DocumentID)))
            {
                var document = _IDocument.GetDocumentByExpenseID(Convert.ToInt32(id), Convert.ToInt32(DocumentID));
                return File(document.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
            }
            else
            {
                return RedirectToAction("Expense", "ShowAllExpense");
            }
        }

        /// <summary>
        /// The InsertExpenseAudit
        /// </summary>
        /// <param name="TimeSheetApproval">The TimeSheetApproval<see cref="ExpenseApprovalModel"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="ExpenseAuditTB"/></returns>
        private ExpenseAuditTB InsertExpenseAudit(ExpenseApprovalModel TimeSheetApproval, int Status)
        {
            try
            {
                ExpenseAuditTB objAuditTB = new ExpenseAuditTB
                {
                    ApprovaExpenselLogID = 0,
                    ExpenseID = TimeSheetApproval.ExpenseID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = TimeSheetApproval.Comment,
                    ApprovalUser = Convert.ToInt32(Session["AdminUser"]),
                    ProcessedDate = DateTime.Now,
                    UserID = _IUsers.GetUserIDbyExpenseID(TimeSheetApproval.ExpenseID)
                };
                return objAuditTB;
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
        /// The LoadExpenseSubmittedData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadExpenseSubmittedData()
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

                var expensedata = _IExpense.ShowAllSubmittedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadExpenseApprovedData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadExpenseApprovedData()
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

                var expensedata = _IExpense.ShowAllApprovedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadExpenseRejectedData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadExpenseRejectedData()
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

                var expensedata = _IExpense.ShowAllRejectedExpense(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = expensedata.Count();
                var data = expensedata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
