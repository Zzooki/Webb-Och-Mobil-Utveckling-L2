using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class TaskController : ApiController
    {
        private static DbtEntities1 db = new DbtEntities1();

        public IEnumerable<TaskDatabaseTable> Get()
        {
            return TaskModel.GetAll();
        }
        public TaskDatabaseTable Get(int id)
        {
            return TaskModel.GetTask(id);
        }

        public List<TaskDatabaseTable> GetUserTasks(int id)
        {
            List<TaskDatabaseTable> userTasks = null;
            List<Assignment> assignmentList = null;

            foreach (var something in db.Assignment)
            {
                if (something.Equals(id))
                    assignmentList.Add(something);
            }
            foreach (var findTask in assignmentList)
            {
                userTasks.Add(TaskModel.GetTask(findTask.TaskID));
            }

            return userTasks;
        }
    }
}