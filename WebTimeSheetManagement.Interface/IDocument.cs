namespace WebTimeSheetManagement.Interface
{
    using System.Collections.Generic;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IDocument" />
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// The AddDocument
        /// </summary>
        /// <param name="Documents">The Documents<see cref="Documents"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddDocument(Documents Documents);

        /// <summary>
        /// The GetDocumentByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int?"/></param>
        /// <param name="DocumentID">The DocumentID<see cref="int?"/></param>
        /// <returns>The <see cref="Documents"/></returns>
        Documents GetDocumentByExpenseID(int? ExpenseID, int? DocumentID);

        /// <summary>
        /// The GetListofDocumentByExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int?"/></param>
        /// <returns>The <see cref="List{DocumentsVM}"/></returns>
        List<DocumentsVM> GetListofDocumentByExpenseID(int? ExpenseID);
    }
}
