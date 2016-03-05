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
    public class AssignmentController : ApiController
    {

        private static DbtEntities1 db = new DbtEntities1();

        [ResponseType(typeof(string))]
        public IHttpActionResult PostAssignment(int userID, int taskID)
        {
            int assignmentOwnedByUser = 0;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach(var column in db.Assignment)
            {
                if (column.UserID == userID && column.TaskID == taskID)
                {
                    assignmentOwnedByUser = 1;
                    return Ok();
                }
                    
            }
            Assignment newAssign = new Assignment();
            newAssign.UserID = userID;
            newAssign.TaskID = taskID;
            db.Assignment.Add(newAssign);
            
            
             // Här behöver vi även lägga till tasken i user och user i tasken.
             //Vi bör även söka i asignment tabellen om den redan vinns innan vi lägger till den.

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

        public IHttpActionResult DeleteAssignment(int userID, int taskID)
        {
            AssignmentModel amodel = new AssignmentModel();
            Assignment assign = amodel.GetAssignment(userID, taskID);

            if (assign == null)
            {
                return NotFound();
            }
            try
            {
                amodel.RemoveAssignment(assign);
            }catch(Exception)
            {
                return InternalServerError();
            }

            return Ok("Du har inte längre ansvar för uppgiften");
        }
    }
}