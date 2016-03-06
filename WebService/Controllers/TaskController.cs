using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// TaskController is a webapi controller to enable the application to retrive information
    /// regarding the different tasks in the database.
    /// </summary>
    public class TaskController : ApiController
    {
        private static DbtEntities1 db = new DbtEntities1();
        /// <summary>
        /// Get method to get all the tasks stored in the database, is usefull for the application.
        /// </summary>
        /// <returns>Returns a IEnumerable containing TaskDatabase objects which is all the tasks stored in the database.</returns>
        public IEnumerable<TaskDatabaseTable> Get()
        {
            return TaskModel.GetAll();
        }

    }
}