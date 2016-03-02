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
        private static DatabasefjortonEntities db = new DatabasefjortonEntities();
        // GET: Tasks
        public ActionResult Index()
        {
            List<TaskDatabaseTable> tasksList = db.TaskDatabaseTable.ToList();

            return View(tasksList);
        }
    }
}