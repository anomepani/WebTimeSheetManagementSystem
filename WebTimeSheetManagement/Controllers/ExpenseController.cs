namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ExpenseController" />
    /// </summary>
    [ValidateUserSession]
    public class ExpenseController : Controller
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
        /// Defines the _IProject
        /// </summary>
        private readonly IProject _IProject;

        /// <summary>
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseController"/> class.
        /// </summary>
        public ExpenseController()
        {
            _IExpense = new ExpenseConcrete();
            _IProject = new ProjectConcrete();
            _IDocument = new DocumentConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: Expense
        /// <summary>
        /// The Add
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Add()
        {
            return View(new ExpenseModel());
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="expensemodel">The expensemodel<see cref="ExpenseModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Add(ExpenseModel expensemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Documents Documents = new Documents();

                    if (_IExpense.CheckIsDateAlreadyUsed(expensemodel.FromDate, expensemodel.ToDate, Convert.ToInt32(Session["UserID"])))
                    {
                        ModelState.AddModelError("", "Date you have choosen is already used !");
                        return View(expensemodel);
                    }
                    else
                    {
                        expensemodel.ExpenseID = 0;
                        expensemodel.CreatedOn = DateTime.Now;
                        expensemodel.ExpenseStatus = 1;
                        expensemodel.UserID = Convert.ToInt32(Session["UserID"]);

                        var ExpenseID = _IExpense.AddExpense(expensemodel);
                        if (ExpenseID > 0)
                        {
                            if (Request.Files != null)
                            {
                                foreach (string requestFile in Request.Files)
                                {
                                    HttpPostedFileBase file = Request.Files[requestFile];
                                    {
                                        if (file.ContentLength > 0)
                                        {
                                            string _FileName = Path.GetFileName(file.FileName);
                                            Documents.DocumentID = 0;
                                            Documents.DocumentName = _FileName;
                                            using (var binaryReader = new BinaryReader(file.InputStream))
                                            {
                                                byte[] FileSize = binaryReader.ReadBytes(file.ContentLength);
                                                Documents.DocumentBytes = FileSize;
                                                Documents.CreatedOn = DateTime.Now;
                                            }

                                            Documents.ExpenseID = ExpenseID;
                                            Documents.UserID = Convert.ToInt32(Session["UserID"]);
                                            if (Path.GetExtension(file.FileName) == ".zip" || Path.GetExtension(file.FileName) == ".rar")
                                            {
                                                Documents.DocumentType = "Multi";
                                            }
                                            else
                                            {
                                                Documents.DocumentType = "Single";
                                            }

                                            _IDocument.AddDocument(Documents);
                                        }

                                        TempData["ExpenseMessage"] = "Data Saved Successfully";
                                    }
                                }
                            }
                            _IExpense.InsertExpenseAuditLog(InsertExpenseAudit(ExpenseID, 1));
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please Upload Required Attachments");
                            return View(expensemodel);
                        }

                        ModelState.Clear();
                        return View(new ExpenseModel());
                    }
                }
                return View(new ExpenseModel());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ListofProjects
        /// </summary>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult ListofProjects()
        {
            try
            {
                var listofProjects = _IProject.GetListofProjects();
                return Json(listofProjects, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The InsertExpenseAudit
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="ExpenseAuditTB"/></returns>
        private ExpenseAuditTB InsertExpenseAudit(int ExpenseID, int Status)
        {
            try
            {
                ExpenseAuditTB objAuditTB = new ExpenseAuditTB
                {
                    ApprovaExpenselLogID = 0,
                    ExpenseID = ExpenseID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = string.Empty,
                    ApprovalUser = _IUsers.GetAdminIDbyUserID(Convert.ToInt32(Session["UserID"])),
                    ProcessedDate = DateTime.Now,
                    UserID = Convert.ToInt32(Session["UserID"])
                };
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
