namespace WebTimeSheetManagement.Concrete
{
    using System.Linq;
    using WebTimeSheetManagement.Interface;

    /// <summary>
    /// Defines the <see cref="RolesConcrete" />
    /// </summary>
    public class RolesConcrete : IRoles
    {
        /// <summary>
        /// Get RoleID Name by RoleName
        /// </summary>
        /// <param name="Rolename"></param>
        /// <returns></returns>
        public int GetRolesofUserbyRolename(string Rolename)
        {
            using (var _context = new DatabaseContext())
            {
                var roleID = (from role in _context.Role
                              where role.Rolename == Rolename
                              select role.RoleID).SingleOrDefault();

                return roleID;
            }
        }
    }
}
