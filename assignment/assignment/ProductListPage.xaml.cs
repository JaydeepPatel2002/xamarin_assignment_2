using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace assignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
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
                
            }
            
            void SettingToolbarItem_Clicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new SettingsPage());
            }
            
            List<Products> list2 = database.GetAllProducts();
            var listView = new ListView
            {
                ItemsSource = list2,
                
                //you have to change this code when u come on monday then change customProductcell.
                
                ItemTemplate = new DataTemplate(typeof(CustomProductCell)),
                RowHeight = CustomProductCell.RowHeight,
                IsPullToRefreshEnabled = true
            };
            Title = "Products";
            StackLayout layout = new StackLayout();
            layout.Children.Add(listView);
            Content = layout;
            // InitializeComponent();
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