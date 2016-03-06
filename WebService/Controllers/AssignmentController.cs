using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// AssignmentController is a web api controller enabling the app to retrive information regarding the 
    /// assignments and also manipulate the assignments.
    /// </summary>
    public class AssignmentController : ApiController
    {

        private static DbtEntities1 db = new DbtEntities1();
        /// <summary>
        /// Get method is used to get all the assignments in the database.
        /// </summary>
        /// <returns>Returns aa IEnumerable containing Assignment objects which is all the assignements in the database</returns>
        public IEnumerable<Assignment> Get()
        {
            return AssignmentModel.GetAll();
        }

        /// <summary>
        /// PostAssignment method is used whenever a user wishes to assign to the task.
        /// </summary>
        /// <param name="userID">userID parameter is the userID on the current user which wants to assign to the task</param>
        /// <param name="taskID">taskID parameter is the task which the user wishes to assign to.</param>
        /// <returns>Returns a IHttpActionResult which signals if the action was successful or not</returns>
        [ResponseType(typeof(string))]
        public IHttpActionResult PostAssignment(int userID, int taskID)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach(var column in db.Assignment)
            {
                if (column.UserID == userID && column.TaskID == taskID)
                {
                    ///The Assignment does already exist, no need for further action. I.e no need to adding another row to the database.
                    return Ok();
                }
                    
            }
            Assignment newAssign = new Assignment();
            newAssign.UserID = userID;
            newAssign.TaskID = taskID;
            db.Assignment.Add(newAssign);
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// DeleteAssignment method is used whenever the user does not wish to be assigned to that task
        /// </summary>
        /// <param name="userID">userID parameter is the id for the current user which does not want to be assigned to that task</param>
        /// <param name="taskID">taskID parameter is the id for the task which the user does not want to be assigned to.</param>
        /// <returns>Returns a IHttpActionResult which signals if the action was successful or not</returns>
        public IHttpActionResult DeleteAssignment(int userID, int taskID)
        {
            Assignment assign = AssignmentModel.GetAssignment(userID, taskID);

            if (assign == null)
            {
                return NotFound();
            }
            try
            {
                AssignmentModel.RemoveAssignment(assign);
            }catch(Exception)
            {
                return InternalServerError();
            }

            return Ok("Du har inte längre ansvar för uppgiften");
        }
    }
}