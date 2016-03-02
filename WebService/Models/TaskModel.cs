﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    class TaskModel
    {
        private static DatabasefjortonEntities db = new DatabasefjortonEntities();
        private static List<TaskDatabaseTable> tasks = new List<TaskDatabaseTable>();

        public static void CreateTask(TaskDatabaseTable task)
        {
            tasks.Add(task);
        }
        public static List<TaskDatabaseTable> GetAll()
        {
            TaskDatabaseTable newTask = new TaskDatabaseTable();

            foreach (var taskVar in db.TaskDatabaseTable)
            {
                newTask.BeginDateTime = taskVar.BeginDateTime;
                newTask.DeadlineDateTime = taskVar.DeadlineDateTime;
                newTask.TaskID = taskVar.TaskID;
                newTask.Title = taskVar.Title;
                newTask.Requirement = taskVar.Requirement;

                tasks.Add(newTask);
            }

            return (tasks);
        }
        public static TaskDatabaseTable GetTask(int id)
        {
            return tasks.Find(uppgift => uppgift.TaskID == id);
        }
        public static void UpdateTask(int id, TaskDatabaseTable uppgift)
        {
            tasks.Remove(tasks.Find(oldTask => oldTask.TaskID == id));
            tasks.Add(uppgift);
        }

        public static void RemoveTask(int id)
        {
            tasks.Remove(tasks.Find(uppgift => uppgift.TaskID == id));
        }
    }
}