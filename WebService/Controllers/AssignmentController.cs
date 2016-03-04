﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Models;

namespace WebService.Controllers
{
    public class AssignmentController : ApiController
    {

        private static DatabasefjortonEntities db = new DatabasefjortonEntities();

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PostAssignment(AssignmentModel assign)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Assignment newAssign = new Assignment();
            newAssign.UserID = assign.user.UserID;
            newAssign.TaskID = assign.userTask.TaskID;
            db.Assignment.Add(newAssign);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}