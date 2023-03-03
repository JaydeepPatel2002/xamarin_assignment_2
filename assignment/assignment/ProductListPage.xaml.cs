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
            List<Products> list2 = database.GetAllProducts();
            var listView = new ListView
            {
                ItemsSource = list2,
                
                //you have to change this code when u come on monday then change customProductcell.
                
              //  ItemTemplate = new DataTemplate(typeof(CustomViewCell)),
               // RowHeight = CustomViewCell.RowHeight,
                IsPullToRefreshEnabled = true
            };
            Title = "Products";

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