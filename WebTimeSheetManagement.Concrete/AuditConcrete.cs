namespace WebTimeSheetManagement.Concrete
{
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="AuditConcrete" />
    /// </summary>
    public class AuditConcrete : IAudit
    {
        /// <summary>
        /// The InsertAuditData
        /// </summary>
        /// <param name="audittb">The audittb<see cref="AuditTB"/></param>
        public void InsertAuditData(AuditTB audittb)
        {
            using (var _context = new DatabaseContext())
            {
                _context.AuditTB.Add(audittb);
                _context.SaveChanges();
            }
        }
    }
}
