using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AssignmentModel
    {
        DbtEntities1 db = new DbtEntities1();
        public User user { get; set; }
        public TaskDatabaseTable userTask { get; set; }

        public Assignment GetAssignment(int userID, int taskID)
        {
            List<Assignment> list = new List<Assignment>();

            list = db.Assignment.ToList();
            Assignment a = list.Find(ac => ac.TaskID == taskID && ac.UserID == userID);

            return a;
        }

        public void RemoveAssignment(Assignment a)
        {
            db.Assignment.Remove(a);
            db.SaveChanges();
        }
    }
}