using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App
{
    public sealed partial class TaskDetailPage : Page
    {
        private TaskData activeTask = new TaskData();
        private Uri apiUri = new Uri("http://localhost:19208/api/assignment");
        private HttpClient client = new HttpClient();

        public TaskDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is TaskData)
            {
                activeTask = (TaskData)e.Parameter;
                taskID.Text = "Task ID: " + activeTask.TaskID;
                taskTitle.Text = "Title: " + activeTask.Title;
                taskBeginDate.Text = "Begin Date" + activeTask.BeginDateTime;
                taskDeadlineDate.Text = "Deadline: " + activeTask.DeadlineDateTime;
                taskRequirement.Text = "Requirements: " + activeTask.Requirement;
            }
        }

        private void makeAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.userTaskList.Add(activeTask);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("taskid", activeTask.TaskID.ToString()),
                new KeyValuePair<string, string>("userid", App.activeUser.UserID.ToString())
            });

            using (var Client = new HttpClient())
            {
                Task task = Task.Run(async () =>
                {
                    await Client.PostAsync(BaseUri, content);
                });
            }

        }

        private void deleteAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
