using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class UserModel
    {
        private static DatabasefjortonEntities db = new DatabasefjortonEntities();
        private static List<User> users = new List<User>();
            
    
        public static void CreateUser(User user)
        {
            users.Add(user);
        }
        public static List<User> GetAll()
        {
            User newUser = new User();

            foreach(var user in db.User)
            {
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.UserID = user.UserID;

                users.Add(newUser);
            }

            return (users);
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
