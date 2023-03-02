using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LO5TableListEx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace assignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractionListPage : ContentPage
    {
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
            layout.Children.Add(listView);
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