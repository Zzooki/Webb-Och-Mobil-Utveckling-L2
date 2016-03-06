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
    /// <summary>
    /// UserController is a controller consumed by the application to enable it to get information
    /// regarding the users.
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// Get method is consumed by the app and is used for getting all available users.
        /// </summary>
        /// <returns>Returns a IEnumerable containing User objects which is all the available users</returns>
        public IEnumerable<User> Get()
        {
            return UserModel.GetAll();
        }
    }
}
