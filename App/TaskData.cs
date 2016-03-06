using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    /// <summary>
    /// Klassen som används för att hantera tasks i appen.
    /// </summary>
    public class TaskData
    {
        public int TaskID { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public string Title { get; set; }
        public string Requirement { get; set; }
    }
}
