namespace WebTimeSheetManagement.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="DocumentConcrete" />
    /// </summary>
    public class DocumentConcrete : IDocument
    {
        /// <summary>
        /// The AddDocument
        /// </summary>
        /// <param name="Documents">The Documents<see cref="Documents"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddDocument(Documents Documents)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.Documents.Add(Documents);
                    _context.SaveChanges();
                    int id = Documents.DocumentID;
                    return id;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        /// <summary>
        /// The GetDocumentByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int?"/></param>
        /// <param name="DocumentID">The DocumentID<see cref="int?"/></param>
        /// <returns>The <see cref="Documents"/></returns>
        public Documents GetDocumentByExpenseID(int? ExpenseID, int? DocumentID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var tempDocument = (from document in _context.Documents
                                        where document.ExpenseID == ExpenseID && document.DocumentID == DocumentID
                                        select document).FirstOrDefault();

                    return tempDocument;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetListofDocumentByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int?"/></param>
        /// <returns>The <see cref="List{DocumentsVM}"/></returns>
        public List<DocumentsVM> GetListofDocumentByExpenseID(int? ExpenseID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var tempDocument = (from document in _context.Documents
                                        where document.ExpenseID == ExpenseID
                                        select new DocumentsVM
                                        {
                                            DocumentID = document.DocumentID,
                                            DocumentName = document.DocumentName,
                                            ExpenseID = document.ExpenseID,
                                            DocumentType = document.DocumentType

                                        }).ToList();

                    return tempDocument;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
