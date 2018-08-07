namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Dynamic;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ProjectConcrete" />
    /// </summary>
    public class ProjectConcrete : IProject
    {
        /// <summary>
        /// The CheckProjectCodeExists
        /// </summary>
        /// <param name="ProjectCode">The ProjectCode<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckProjectCodeExists(string ProjectCode)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.ProjectMaster
                                  where user.ProjectCode == ProjectCode
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckProjectNameExists
        /// </summary>
        /// <param name="ProjectName">The ProjectName<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckProjectNameExists(string ProjectName)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.ProjectMaster
                                  where user.ProjectName == ProjectName
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetListofProjects
        /// </summary>
        /// <returns>The <see cref="List{ProjectMaster}"/></returns>
        public List<ProjectMaster> GetListofProjects()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofProjects = (from project in _context.ProjectMaster
                                          select project).ToList();
                    return listofProjects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The SaveProject
        /// </summary>
        /// <param name="ProjectMaster">The ProjectMaster<see cref="ProjectMaster"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int SaveProject(ProjectMaster ProjectMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.ProjectMaster.Add(ProjectMaster);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ShowProjects
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{ProjectMaster}"/></returns>
        public IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new DatabaseContext();

            var IQueryableproject = (from projectmaster in _context.ProjectMaster
                                     select projectmaster);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableproject = IQueryableproject.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableproject = IQueryableproject.Where(m => m.ProjectName == Search || m.ProjectCode == Search);
            }

            return IQueryableproject;
        }

        /// <summary>
        /// The CheckProjectIDExistsInTimesheet
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckProjectIDExistsInTimesheet(int ProjectID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from timesheet in _context.TimeSheetDetails
                                  where timesheet.ProjectID == ProjectID
                                  select timesheet).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckProjectIDExistsInExpense
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckProjectIDExistsInExpense(int ProjectID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from expense in _context.ExpenseModel
                                  where expense.ProjectID == ProjectID
                                  select expense).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ProjectDelete
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int ProjectDelete(int ProjectID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var project = (from expense in _context.ProjectMaster
                                   where expense.ProjectID == ProjectID
                                   select expense).SingleOrDefault();

                    if (project != null)
                    {
                        _context.ProjectMaster.Remove(project);
                        int resultProject = _context.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetTotalProjectsCounts
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int GetTotalProjectsCounts()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var Count = con.Query<int>("Usp_GetProjectCount", null, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
                if (Count > 0)
                {
                    return Count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
