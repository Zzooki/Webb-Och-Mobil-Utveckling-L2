using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App
{
    public sealed partial class TaskPage : Page
    {
        private static Uri BaseUri = new Uri("http://localhost:19208/api/task");
        public TaskPage()
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
                taskList.ItemsSource = list;
            }
        }
    }
}
