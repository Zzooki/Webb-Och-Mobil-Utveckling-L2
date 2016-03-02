using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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

        private async void makeAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.userTaskList.Add(activeTask);

            HttpResponseMessage response = null;
            AssignmentClass newAssign = new AssignmentClass
            {
                user = App.activeUser,
                userTask = activeTask
            };
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(newAssign);

                Task task = Task.Run(async () =>
                {
                    StringContent name = new StringContent(json);
                    response = await client.PostAsync(apiUri, name);
                });
                task.Wait();
            }
            if (response.ReasonPhrase.Equals("Not Found"))
            {
                var dialog = new MessageDialog("User already assigned to task");
                await dialog.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            this.Frame.Navigate(typeof(MainPage));

            /*HttpResponseMessage response = null;

            Assignment test = new Assignment
            {
                TaskID = user.TaskID,
                UserID = App.user.UserID
            };
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(test);

                Task task = Task.Run(async () =>
                {
                    StringContent till = new StringContent(json);
                    response = await client.PostAsync(App.BaseUri + "api/Assignments?UserId=" + App.user.UserID + "&TaskID=" + user.TaskID, till);
                });
                task.Wait();
            }
            if (response.ReasonPhrase.Equals("Not Found"))
            {
                var dialog = new MessageDialog("User already assigned to task");
                await dialog.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            this.Frame.Navigate(typeof(MainPage));
        }*/

        }

        private void deleteAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
