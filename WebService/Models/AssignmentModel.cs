using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    /// <summary>
    /// AssignmentModel class is used for communication between the webapi controller and the database.
    /// </summary>
    public class AssignmentModel
    {
        private static DbtEntities1 db = new DbtEntities1();

        /// <summary>
        /// GetAssignment method is used for retrieving a assignment for a specific user from the database in order to se
        /// if the user is assigned to that task.
        /// </summary>
        /// <param name="userID">userID is the parameter for the user which might be assigned to the task</param>
        /// <param name="taskID">taskID is the parameter for the task</param>
        /// <returns>Returns a assignment object if there is a assignment between the task and the user</returns>
        public static Assignment GetAssignment(int userID, int taskID)
        {
            List<Assignment> list = new List<Assignment>();

            list = db.Assignment.ToList();
            Assignment a = list.Find(ac => ac.TaskID == taskID && ac.UserID == userID);

            return a;
        }
        /// <summary>
        /// GetAll method is used to retrieve all assignments from the database
        /// </summary>
        /// <returns>Returns a list containing Assignment objects which is the assignments stored in the database</returns>
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
        /// <summary>
        /// RemoveAssignment method is used to remove the connection between a user and a task in the database, I.e a assignment.
        /// </summary>
        /// <param name="a">The parameter is the assignment which needs to be removed from the database</param>
        public static void RemoveAssignment(Assignment a)
        {
            db.Assignment.Remove(a);
            db.SaveChanges();
        }
    }
}