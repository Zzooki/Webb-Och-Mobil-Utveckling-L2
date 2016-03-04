using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class UserModel
    {
        private static DbtEntities1 db = new DbtEntities1();
        private static List<User> users = new List<User>();


        public static void CreateUser(User user)
        {
            users.Add(user);
        }
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
        public static User GetUser(int id)
        {
            return users.Find(user => user.UserID == id);
        }
        public static void UpdateUser(int id, User user)
        {
            users.Remove(users.Find(oldUser => oldUser.UserID == id));
            users.Add(user);
        }

        public static void RemoveUser(int id)
        {
            users.Remove(users.Find(user => user.UserID == id));
        }

    }
}
