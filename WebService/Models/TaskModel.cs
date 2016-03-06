using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    /// <summary>
    /// TaskModel class is used for the communication between the user controller,
    /// and the database.
    /// </summary>
    class TaskModel
    {
        private static DbtEntities1 db = new DbtEntities1();

        
        /// <summary>
        /// GetAll method is used for getting all tasks stored in the database.
        /// </summary>
        /// <returns>Returns all the tasks in the database as a list</returns>
        public static List<TaskDatabaseTable> GetAll()
        {
            List<TaskDatabaseTable> thisList = new List<TaskDatabaseTable>();

            foreach (var taskVar in db.TaskDatabaseTable)
            {
                TaskDatabaseTable newTask = new TaskDatabaseTable();

                newTask.BeginDateTime = taskVar.BeginDateTime;
                newTask.DeadlineDateTime = taskVar.DeadlineDateTime;
                newTask.TaskID = taskVar.TaskID;
                newTask.Title = taskVar.Title;
                newTask.Requirement = taskVar.Requirement;

                thisList.Add(newTask);
            }

            return (thisList);
        }
        
    }
}
