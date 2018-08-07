namespace WebTimeSheetManagement.Interface
{
    /// <summary>
    /// Defines the <see cref="IRoles" />
    /// </summary>
    public interface IRoles
    {
        /// <summary>
        /// The GetRolesofUserbyRolename
        /// </summary>
        /// <param name="Rolename">The Rolename<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetRolesofUserbyRolename(string Rolename);
    }
}
