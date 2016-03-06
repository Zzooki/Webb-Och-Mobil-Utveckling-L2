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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserTaskView : Page
    {

        private static Uri BaseUri = new Uri("http://localhost:19208/api/task");

        public UserTaskView()
        {
            this.InitializeComponent();

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
                            if (item.TaskID == a.taskID && a.userID == App.activeUser.UserID)
                            {
                                App.userTaskList.Add(item);
                            }
                        }
                    }
                }
            }
            taskList.ItemsSource = App.userTaskList;

                //* När vi får tillbaka svar som är användarens lista, glöm inte att spara den i appen lokalt
                //Dvs raden nedan ska läggas till:
                //App.userTaskList = list;
            }


        }
    }

