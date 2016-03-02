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
    public sealed partial class TaskDetailPage : Page
    {
        private TaskData activeTask = new TaskData();
        private Uri apiUri = new Uri("http://localhost:19208/api/task");
        private HttpClient client = new HttpClient();

        public TaskDetailPage()
        {
            this.InitializeComponent();
        }
    }
}
