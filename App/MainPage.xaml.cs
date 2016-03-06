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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App
{
    /// <summary>
    /// Appens första sida. Där användarna som finns i databasen visas och den aktiva användaren väljs.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Uri BaseUri = new Uri("http://localhost:19208/api/user");
        /// <summary>
        /// Initiering av första sidan.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            using (var Client = new HttpClient())
            {
                try
                {
                    var response = "";
                    Task task = Task.Run(async () =>
                    {
                        response = await Client.GetStringAsync(BaseUri);

                    });
                    task.Wait();
                    List<User> list = JsonConvert.DeserializeObject<List<User>>(response);
                    userList.ItemsSource = list;
                }
                catch (AggregateException ex)
                {
                    System.Diagnostics.Debug.WriteLine("One or more exceptions has occurred:");
                    foreach (var exception in ex.InnerExceptions)
                    {
                        System.Diagnostics.Debug.WriteLine("  " + exception.Message);
                    }
                }
            }
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        /// <summary>
        /// Metod för att lagra den aktiva användaren lokalt i appen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            User activeUser = userList.SelectedItem as User;
            App.activeUser = activeUser;
            this.Frame.Navigate(typeof(TaskPage));
        }
        /// <summary>
        /// Metod för att gå tillbaka till sidan där samtliga tasks visas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskPage));
        }

     
    }
}
