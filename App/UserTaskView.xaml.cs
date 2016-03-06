using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App
{
    /// <summary>
    /// Sida för att visa de tasks som den aktiva användaren har ansvar för.
    /// </summary>
    public sealed partial class UserTaskView : Page
    {

        private static Uri BaseUri = new Uri("http://localhost:19208/api/task");
        /// <summary>
        /// Initiering av sidan.
        /// </summary>
        public UserTaskView()
        {
            this.InitializeComponent();

            App.userTaskList = new List<TaskData>();

            using (var Client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await Client.GetStringAsync(BaseUri);
                });
                task.Wait();
                List<TaskData> list = JsonConvert.DeserializeObject<List<TaskData>>(response);
                using (var Client2 = new HttpClient())
                {
                    var response2 = "";
                    Task task2 = Task.Run(async () =>
                    {
                        response2 = await Client2.GetStringAsync("http://localhost:19208/api/assignment");
                    });
                    task2.Wait();
                    List<AssignmentClass> aList = JsonConvert.DeserializeObject<List<AssignmentClass>>(response2);
                    taskList.ItemsSource = aList;
                    foreach (var item in list)
                    {
                        foreach (var a in aList)
                        {
                            if (item.TaskID == a.taskID && a.userID == App.activeUser.UserID && !App.userTaskList.Contains(item))
                            {
                                App.userTaskList.Add(item);
                            }
                        }
                    }
                }
            }
            taskList.ItemsSource = App.userTaskList;

        }
        /// <summary>
        /// Metod för att visa detaljerad information om en task, från listan av tasks som den aktiva
        /// användaren har ansvar för.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskData selectedItem = taskList.SelectedItem as TaskData;

            if (selectedItem == null)
            {
                return;
            }
            this.Frame.Navigate(typeof(TaskDetailPage), selectedItem);
        }
        /// <summary>
        /// Metod för att gå tillbaka till taskpage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskPage));
        }
    }
    }

