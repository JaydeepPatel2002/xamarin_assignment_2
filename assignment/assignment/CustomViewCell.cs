using System.Collections.ObjectModel;
using Android.Widget;
using assignment;
using Xamarin.Forms;
namespace LO5TableListEx
{
    public class CustomViewCell : ViewCell
    {
        public const int RowHeight = 55;
        
        public CustomViewCell()
        {
            var fNameLabel = new Label { FontAttributes = FontAttributes.Bold };
            fNameLabel.SetBinding(Label.TextProperty, "FirstName");

            var lNameLabel = new Label { TextColor = Color.Gray };
            lNameLabel.SetBinding(Label.TextProperty, "LastName");
            
            var PhoneLabel = new Label { TextColor = Color.Gray };
            PhoneLabel.SetBinding(Label.TextProperty, "Phone");
            // Create a SwipeView control
            SwipeView swipeView = new SwipeView
            {
                LeftItems = new SwipeItems
                {
                    // Add left swipe items
                    new SwipeItem
                    {
                        Text = "Delete",
                        BackgroundColor = Color.Red,
                        IconImageSource = "delete.png",
                        Command = new Command(() => DeleteItem((Customers)BindingContext))
                        //Command = new Command(() =>Toast.MakeText(Android.App.Application.Context, "you got some serious success", ToastLength.Short)?.Show())
                    }
                },
                RightItems = new SwipeItems
                {
                    // Add right swipe items
                    new SwipeItem
                    {
                        Text = "Flag",
                        BackgroundColor = Color.Yellow,
                        IconImageSource = "flag.png",
                        //Command = new Command(() => FlagItem((MyModel)BindingContext))
                        Command = new Command(() =>Toast.MakeText(Android.App.Application.Context, "you got some serious reward", ToastLength.Short)?.Show())
                    }
                },
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    // Add content items
                    Children =
                    {
                        fNameLabel,
                        lNameLabel,
                        PhoneLabel
                        // new Label
                        // {
                        //     Text = "Title",
                        //     FontAttributes = FontAttributes.Bold
                        // },
                        // new Label
                        // {
                        //     Text = "Description"
                        // }
                    }
                }
            };

            // Set the View property to the SwipeView control
            View = swipeView;
        }

        private void DeleteItem(Customers item)
        {
            database = Database;
            database.DeleteCustomer(item);

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
        //
        // private void FlagItem(MyModel item)
        // {
        //     // Flag the selected item
        // }
    }

}