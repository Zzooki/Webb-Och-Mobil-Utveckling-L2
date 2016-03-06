using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AssignmentModel
    {
        private static DbtEntities1 db = new DbtEntities1();
        public User user { get; set; }
        public TaskDatabaseTable userTask { get; set; }

        public static Assignment GetAssignment(int userID, int taskID)
        {
            List<Assignment> list = new List<Assignment>();

            list = db.Assignment.ToList();
            Assignment a = list.Find(ac => ac.TaskID == taskID && ac.UserID == userID);

            return a;
        }
        public static List<Assignment> GetAll()
        {
            List<Assignment> aList = new List<Assignment>();
            foreach (var item in db.Assignment)
            {
                Assignment newAssign = new Assignment();
                newAssign.AssignmentID = item.AssignmentID;
                newAssign.TaskID = item.TaskID;
                newAssign.UserID = item.UserID;

                aList.Add(newAssign);
            }
            return aList;
        }

        public static void RemoveAssignment(Assignment a)
        {
            db.Assignment.Remove(a);
            db.SaveChanges();
        }
    }
}