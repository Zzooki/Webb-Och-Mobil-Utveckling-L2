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

        private static Uri BaseUri = new Uri("http://localhost:19208/api/manage");

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
                taskList.ItemsSource = list;

                //* När vi får tillbaka svar som är användarens lista, glöm inte att spara den i appen lokalt
                //Dvs raden nedan ska läggas till:
                //App.userTaskList = list;
            }


        }
    }
}

