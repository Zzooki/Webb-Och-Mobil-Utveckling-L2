using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// TasksController is a controller to enable the website to show information regarding the tasks in the 
    /// database and the users assigned to them.
    /// </summary>
    public class TasksController : Controller
    {
        private static DbtEntities1 db = new DbtEntities1();
        // GET: Tasks
        /// <summary>
        /// Index method is consumed by the website to display all the task and the users assigned to them
        /// </summary>
        /// <returns>Returns the view repesenting all the tasks and the users assigned to them</returns>
        public ActionResult Index()
        {
            List<TaskDatabaseTable> tasksList = db.TaskDatabaseTable.ToList();

            return View(tasksList);
        }
    }
}