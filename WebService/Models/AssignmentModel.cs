using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AssignmentModel
    {
        public User user { get; set; }
        public TaskDatabaseTable userTask { get; set; }
    }
}