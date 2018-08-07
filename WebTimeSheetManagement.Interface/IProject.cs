namespace WebTimeSheetManagement.Interface
{
    using System.Collections.Generic;
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IProject" />
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// The GetListofProjects
        /// </summary>
        /// <returns>The <see cref="List{ProjectMaster}"/></returns>
        List<ProjectMaster> GetListofProjects();

        /// <summary>
        /// The CheckProjectCodeExists
        /// </summary>
        /// <param name="ProjectCode">The ProjectCode<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckProjectCodeExists(string ProjectCode);

        /// <summary>
        /// The CheckProjectNameExists
        /// </summary>
        /// <param name="ProjectName">The ProjectName<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckProjectNameExists(string ProjectName);

        /// <summary>
        /// The SaveProject
        /// </summary>
        /// <param name="ProjectMaster">The ProjectMaster<see cref="ProjectMaster"/></param>
        /// <returns>The <see cref="int"/></returns>
        int SaveProject(ProjectMaster ProjectMaster);

        /// <summary>
        /// The ShowProjects
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{ProjectMaster}"/></returns>
        IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The CheckProjectIDExistsInTimesheet
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckProjectIDExistsInTimesheet(int ProjectID);

        /// <summary>
        /// The CheckProjectIDExistsInExpense
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckProjectIDExistsInExpense(int ProjectID);

        /// <summary>
        /// The ProjectDelete
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int ProjectDelete(int ProjectID);

        /// <summary>
        /// The GetTotalProjectsCounts
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int GetTotalProjectsCounts();
    }
}
