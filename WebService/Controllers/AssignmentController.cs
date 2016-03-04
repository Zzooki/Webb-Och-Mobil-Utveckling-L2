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

        private static DatabasefjortonEntities db = new DatabasefjortonEntities();

        [ResponseType(typeof(string))]
        public IHttpActionResult PostAssignment(int userID, int taskID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return Ok("Du har nu ansvar för uppgiften");
        }

        public IHttpActionResult DeleteAssignment(int userID, int taskID)
        {
            Assignment assign = db.Assignment.Find(userID, taskID);
            if (assign == null)
            {
                return NotFound();
            }
            db.Assignment.Remove(assign);
            db.SaveChanges();
            return Ok("Du har inte längre ansvar för uppgiften");
        }
    }
}