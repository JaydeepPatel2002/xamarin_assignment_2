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
    public partial class AddCustomerPage : ContentPage
    {
        public AddCustomerPage()
        {
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
            Title = "constructor new customer";
            Content = new Label
            {
                Text = "here is a description",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            
            Title = "new customer";
            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.Center };

            TableView table = new TableView { Intent = TableIntent.Form };
            
            EntryCell fName = new EntryCell { Label = "FirstName", Placeholder = "enter ur first name" };
            EntryCell lName = new EntryCell { Label = "LastName", Placeholder = "enter ur last name" };
            EntryCell Address = new EntryCell { Label = "Address", Placeholder = "enter ur address" };
            EntryCell Phone = new EntryCell { Label = "Phone", Placeholder = "enter ur phone" };
            EntryCell Email = new EntryCell { Label = "Email", Placeholder = "enter ur email" };
            TableSection section = new TableSection("Add new customer")
            {
                fName, lName, Address, Phone, Email
            };
            table.Root = new TableRoot { section};
            Button btnSave = new Button { Text = "Save" };
            btnSave.Clicked += (sender, e) =>
            {
                database = Database;

                Customers tempCustomer = new Customers()
                {
                    FirstName = fName.Text,
                    LastName = lName.Text,
                    Address = Address.Text,
                    Phone = Phone.Text,
                    Email = Email.Text
                };

                database.SaveCustomer(tempCustomer);
                
                List<Customers> list2 = database.GetAllCustomers();

                //Toast.MakeText(Android.App.Application.Context, string.Join(", ", list2.Select(x => x.FirstName)), ToastLength.Short)?.Show();

                
                Navigation.PopAsync();
            };
            Button btnCancel = new Button { Text = "Cancel" };
            btnCancel.Clicked += (sender, e) => { Navigation.PopAsync(); };

            layout.Children.Add(table);
            layout.Children.Add(btnSave);
            layout.Children.Add(btnCancel);
            Content = layout;
            
            
            
            
            
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