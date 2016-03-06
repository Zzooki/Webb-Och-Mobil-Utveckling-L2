using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    /// <summary>
    /// UserModel class is used for the communication between the user controller,
    /// and the database.
    /// </summary>
    public class UserModel
    {
        private static DbtEntities1 db = new DbtEntities1();

        /// <summary>
        /// GetAll method is used to return all the users in the database as a list.
        /// </summary>
        /// <returns>a list containing User objects</returns>
        public static List<User> GetAll()
        {
            List<User> thisList = new List<User>();

            foreach (var user in db.User)
            {
                User newUser = new User();
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.UserID = user.UserID;

                thisList.Add(newUser);
            }

            return (thisList);
        }
       
    }
}
