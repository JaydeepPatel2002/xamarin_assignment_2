using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;

namespace assignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            Button myButton = new Button
            {
                Text = "Reset App"
            };

            // Add a click event handler for the button
            myButton.Clicked += OnButtonClicked;

            // Create a new stack layout to hold the button
            StackLayout myStackLayout = new StackLayout
            {
                Children = { myButton }
            };

            // Set the stack layout as the content for the page
            Content = myStackLayout;
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            database = Database;
            //Toast.MakeText(Android.App.Application.Context, " setting page activated ", ToastLength.Short)?.Show();
            database.resetDatabase();
        }
        static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    var file_path = DependencyService.Get<IFileHelper>().GetLocalFilePath("assignment.db3");
                    database = new Database(file_path);
                }
                return database;
            }
        }
    }
}