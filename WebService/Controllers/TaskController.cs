﻿using System.Collections.Generic;
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
        public IEnumerable<TaskDatabaseTable> Get()
        {
            return TaskModel.GetAll();
        }
        public TaskDatabaseTable Get(int id)
        {
            return TaskModel.GetTask(id);
        }
    }
}