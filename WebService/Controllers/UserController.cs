using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class UserController : ApiController
    {
        //[System.Web.Http.HttpGet]
        public IEnumerable<User> Get()
        {
            return UserModel.GetAll();
        }
        //[System.Web.Http.HttpGet("{id}")]
        //public User Get(int id)
        //{
        //    return UserModel.GetUser(id);
        //}
    }
}
