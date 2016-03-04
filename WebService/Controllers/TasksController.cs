using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class TasksController : Controller
    {
        private static DbtEntities1 db = new DbtEntities1();
        // GET: Tasks
        public ActionResult Index()
        {
            List<TaskDatabaseTable> tasksList = db.TaskDatabaseTable.ToList();

            return View(tasksList);
        }
    }
}