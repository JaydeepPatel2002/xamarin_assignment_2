using System;
using System.Collections.ObjectModel;
//using Android.Widget;
using assignment;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace LO5TableListEx
{
    public class CustomInteractionCell : ViewCell
    {
        public const int RowHeight = 80;
        
        public CustomInteractionCell()
        {
            var fNameLabel = new Label { FontAttributes = FontAttributes.Bold };
            fNameLabel.SetBinding(Label.TextProperty, "CustFirstName");
            
            var lNameLabel = new Label { FontAttributes = FontAttributes.Bold };
            lNameLabel.SetBinding(Label.TextProperty, "CustLastName");

            var dateLabel = new Label { TextColor = Color.Gray };
            dateLabel.SetBinding(Label.TextProperty, "Date", stringFormat: "{0:D}");
            
            var commentLabel = new Label { TextColor = Color.Gray };
            commentLabel.SetBinding(Label.TextProperty, "Comments");
            
            var productLabel = new Label { TextColor = Color.Gray };
            productLabel.SetBinding(Label.TextProperty, "ProDuctName");
            
            Switch purchasedLabel = new Switch { HorizontalOptions=LayoutOptions.Start, IsEnabled=false};
            purchasedLabel.SetBinding(Switch.IsToggledProperty, "Purchased");
            StackLayout switchStack = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions=LayoutOptions.Start };
            switchStack.Children.Add(new Label { Text = "Completed?" });
            switchStack.Children.Add(purchasedLabel);
            
            
            //========================

            StackLayout stack1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { fNameLabel, lNameLabel }
            };
            StackLayout stack2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { dateLabel, commentLabel }
            };
            StackLayout stack3 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { productLabel, switchStack }
            };
            //======================
            
            
            
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
                        Command = new Command(() => DeleteItem((Interactions)BindingContext))
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
                        Command = new Command(() => FlagItem((Interactions)BindingContext))
                        //Command = new Command(() =>Toast.MakeText(Android.App.Application.Context, "you got some serious reward", ToastLength.Short)?.Show())
                    }
                },
                Content = new StackLayout
                {
                    
                    // Add content items
                    Children =
                    {
                        stack1,
                        stack2,
                        stack3
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

        private void DeleteItem(Interactions item)
        {
            database = Database;
            database.DeleteInteractions(item);
            InteractionListPage.listView.ItemsSource = null;
            InteractionListPage.listView.ItemsSource = database.GetInteractionsOfCustomer(item.CustomerID);

        }
        
        private void FlagItem(Interactions item)
        {
            database = Database;
            int id = item.ProductID;

            Products num2 = database.GetOneProduct(id);
            int num = num2.NumInteractions;
            String temp = "num is :- " + num; 
            //Toast.MakeText(Android.App.Application.Context, "num" + num, ToastLength.Short)?.Show();
            // Flag the selected item
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
       
    }

}