namespace WebTimeSheetManagement.Interface
{
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IAudit" />
    /// </summary>
    public interface IAudit
    {
        /// <summary>
        /// The InsertAuditData
        /// </summary>
        /// <param name="audittb">The audittb<see cref="AuditTB"/></param>
        void InsertAuditData(AuditTB audittb);
    }
}
