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
            

            HttpResponseMessage response = null;
            AssignmentClass newAssign = new AssignmentClass
            {
                userID = App.activeUser.UserID,
                taskID = activeTask.TaskID
            };
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(newAssign);

                Task task = Task.Run(async () =>
                {
                    StringContent name = new StringContent(json);
                    response = await client.PostAsync(apiUri + "?UserID="  + App.activeUser.UserID + " &TaskID=" + activeTask.TaskID, name);
                });
                task.Wait();
            }
            if (response.ReasonPhrase.Equals("Not Found"))
            {
                var dialog = new MessageDialog("Something went wrong!");
                await dialog.ShowAsync();
            }
            else
            {
                
                if(response.ReasonPhrase.ToString() == "OK")
                {
                    textBlockResult.Text = "Du är ansvarig för uppgiften";
                    if (App.userTaskList.Contains(activeTask))
                    {
                        this.Frame.Navigate(typeof(TaskPage));
                    }
                    else
                        App.userTaskList.Add(activeTask);
                        this.Frame.Navigate(typeof(TaskPage));
                }
                
            }

        }

        private async void deleteAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.userTaskList.Remove(activeTask);

            HttpResponseMessage response = null;
            AssignmentClass newAssign = new AssignmentClass
            {
                userID = App.activeUser.UserID,
                taskID = activeTask.TaskID
            };
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(newAssign);

                Task task = Task.Run(async () =>
                {
                    StringContent name = new StringContent(json);
                    response = await client.DeleteAsync(apiUri + "?UserID=" + App.activeUser.UserID + " &TaskID=" + activeTask.TaskID);
                });
                task.Wait();
            }
            if (response.ReasonPhrase.Equals("Not Found"))
            {
                var dialog = new MessageDialog("User not assigned to task");
                await dialog.ShowAsync();
            }
            else
            {
                textBlockResult.Text = response.ToString();
            }
            this.Frame.Navigate(typeof(TaskPage));
        }

        private void back_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskPage));
        }
    }
}
