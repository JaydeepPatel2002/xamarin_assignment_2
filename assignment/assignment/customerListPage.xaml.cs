using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using LO5TableListEx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;
using ListView = Xamarin.Forms.ListView;
namespace assignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class customerListPage : ContentPage
    {
        
        public static ListView listView = new ListView();

        public customerListPage()
        {
            database = Database;
            ToolbarItem tb = new ToolbarItem { Text="products"};
            ToolbarItem tb2 = new ToolbarItem { Text="settings"};
            ToolbarItems.Add(tb);
            ToolbarItems.Add(tb2);

            tb.Clicked += ProductsToolbarItem_Clicked;
            tb2.Clicked += SettingToolbarItem_Clicked;
            
            void ProductsToolbarItem_Clicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new ProductListPage());
            }
            
            void SettingToolbarItem_Clicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new SettingsPage());
            }
            // StackLayout layout = new StackLayout();
            // //List<Products> list = database.GetAllProducts();
            List<Customers> list2 = database.GetAllCustomers(); 
            listView = new ListView
            {
                ItemsSource = list2,
                ItemTemplate = new DataTemplate(typeof(CustomViewCell)),
                RowHeight = CustomViewCell.RowHeight,
                IsPullToRefreshEnabled = true
            };
            //===========Important code but not for now=====================================================================================
             listView.ItemTapped += (sender, e) => {
                 listView.SelectedItem = null;
                 Navigation.PushAsync(new InteractionListPage(e.Item as Customers));
                 //((ObservableCollection<Fruit>)listView.ItemsSource).Remove((Fruit)e.Item);
             };
            Title = "Customers";
            //================================================================================================
            StackLayout layout = new StackLayout();
            
             Button btnAddCust = new Button { Text = "Add new Customer" };
             btnAddCust.Clicked += (sender, e)=>
            {
                
                Toast.MakeText(Android.App.Application.Context, string.Join(", ", list2.Select(x => x.ID)), ToastLength.Short)?.Show();
                Navigation.PushAsync(new AddCustomerPage());

            };
            layout.Children.Add(listView);
            layout.Children.Add(btnAddCust);
            Content = layout;
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