using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using assignment;
using LO5TableListEx;
//using LO5TableListEx;
using SQLitePCL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;
using DatePicker = Xamarin.Forms.DatePicker;
using ListView = Xamarin.Forms.ListView;
using Switch = Xamarin.Forms.Switch;

namespace assignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractionListPage : ContentPage
    {
        private int productID;
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker && picker.SelectedItem != null)
            {
                var selectedProduct = (Products)picker.SelectedItem;
                
                string productName = selectedProduct.ProductName;

                productID = selectedProduct.ID;
                string productDescription = selectedProduct.Description;
                // Use the selected product attributes as needed
            }
        }
        
        
        public InteractionListPage(Customers customer)
        {
            database = Database;
            List<Interactions> list2 = database.GetInteractionsOfCustomer(customer.ID);
            var listView = new ListView
            {
                ItemsSource = list2,
                ItemTemplate = new DataTemplate(typeof(CustomInteractionCell)),
                RowHeight = CustomInteractionCell.RowHeight,
                IsPullToRefreshEnabled = true
            };
            //===============================================
            
            //space for some imp code
            
            //================================================
            Title = "Interactions..";
            StackLayout layout = new StackLayout();
            
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            // grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            // grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            
            
            layout.Children.Add(listView);
            
            DatePicker dDate = new DatePicker {Date = DateTime.Now, WidthRequest = 125};
            
            // Entry cDate = new Entry
            // {
            //     View = new StackLayout
            //     {
            //         Orientation = StackOrientation.Horizontal,
            //         Children = {new Label {Text = "Date:"}, dDate}
            //
            //     }
            // };
            Entry Comment = new Entry { Placeholder = "enter ur comment" };
            Picker prodList = new Picker();
            prodList.Title = "choose your product";
            prodList.ItemsSource = database.GetAllProducts();
            prodList.ItemDisplayBinding = new Binding("ProductName");
            prodList.SelectedIndexChanged += OnPickerSelectedIndexChanged;
            
            Switch swtCompleted = new Switch { HorizontalOptions=LayoutOptions.Start, IsEnabled=true};
            //swtCompleted.SetBinding(Switch.IsToggledProperty, "Completed");
            StackLayout switchStack = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions=LayoutOptions.Start };
            switchStack.Children.Add(new Label { Text = "purchased?" });
            switchStack.Children.Add(swtCompleted);
            
            Button btnSave = new Button { Text = "Add" };
            btnSave.Clicked += (sender, e) =>
            {
                database = Database;

                Interactions tempInteraction = new Interactions()
                {
                    CustomerID = customer.ID,
                    Date = dDate.Date,
                    Comments = Comment.Text,
                    ProductID = productID,
                    Purchased = swtCompleted.IsToggled
                    
                };

                database.SaveInteraction(tempInteraction);
                
                List<Interactions> list3 = database.GetInteractionsOfCustomer(customer.ID);

                Toast.MakeText(Android.App.Application.Context, string.Join(", ", list3.Select(x => x.Comments)), ToastLength.Short)?.Show();

                
                //Navigation.PopAsync();
            };
            
            
            
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection("Add new Interaction")
                    {
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.EndAndExpand,
                                Children = {
                                    new Label { Text = "Date:", VerticalOptions = LayoutOptions.EndAndExpand },
                                    dDate
                                }
                            }
                        },
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = {
                                    new Label { Text = "Product:", VerticalOptions = LayoutOptions.EndAndExpand },
                                    prodList
                                }
                            }
                        },
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = {
                                    new Label { Text = "Comment:", VerticalOptions = LayoutOptions.EndAndExpand },
                                    Comment
                                }
                            }
                        },
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.EndAndExpand,
                                Children = {
                                    switchStack
                                }
                            }
                        },
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Children = {
                                    btnSave
                                }
                            }
                        }
                    }
                }
            };
            
            
            // layout.Children.Add(new TableView{Intent = TableIntent.Form, Root = 
            //     new TableRoot{
            //         new TableSection("Fuel Purchase"){ cDate, Comment } }});
            //layout.Children.Add(dDate);
            //layout.Children.Add(Comment);
            //StackLayout temp = new StackLayout();
            //temp.VerticalOptions = LayoutOptions.EndAndExpand;
            layout.VerticalOptions = LayoutOptions.EndAndExpand;
            // temp.Children.Add(tableView);
            // layout.Children.Add(temp);
            
            grid.Children.Add(listView, 0, 0);
            grid.Children.Add(tableView, 0, 1);

            
            
            
            Content = grid;
            //InitializeComponent();
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