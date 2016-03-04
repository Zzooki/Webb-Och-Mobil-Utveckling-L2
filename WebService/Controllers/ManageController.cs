using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class ManageController : ApiController
    {
        DbtEntities1 db = new DbtEntities1();
        //public IEnumerable<TaskDatabaseTable> Get()
        //{
        //    List<TaskDatabaseTable> dbt = db.TaskDatabaseTable.ToList();
        //    return dbt;
        //}
        public IEnumerable<TaskDatabaseTable> Get(int id)
        {
            List<TaskDatabaseTable> userTasks = new List<TaskDatabaseTable>();
            List<Assignment> assignmentList = new List<Assignment>();

            foreach (var something in db.Assignment)
            {
                if (something.UserID.Equals(id))
                    assignmentList.Add(something);
            }
            foreach (var findTask in assignmentList)
            {
                userTasks.Add(db.TaskDatabaseTable.Find(findTask.TaskID));
            }

            return userTasks;
        }
    }
}
