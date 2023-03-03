using System;
using SQLite;
using Xamarin.Forms;

namespace assignment
{
    public class Interactions
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } // Auto-incrementing integer ID
        public int CustomerID { get; set; } // Customer ID - refers to the associated customer
        
        public String CustFirstName
        {
            get
            {
                // compute the value of the property
                database = Database;
                Customers Cust2 = database.GetOneCustomer(CustomerID);
                return Cust2.FirstName;
                
            }
        } 
        public String CustLastName
        {
            get
            {
                // compute the value of the property
                database = Database;
                Customers Cust2 = database.GetOneCustomer(CustomerID);
                return Cust2.LastName;
                
            }
        } 
        public DateTime Date { get; set; } // Date of the interaction
        public string Comments { get; set; } // Comments about the interaction
        public int ProductID { get; set; } // Product ID - refers to the associated product
        
        public String ProDuctName
        {
            get
            {
                // compute the value of the property
                database = Database;
                Products Cust2 = database.GetOneProduct(ProductID);
                return Cust2.ProductName;
                
            }
        } 
        public bool Purchased { get; set; } // Indicates whether the customer purchased the product or not
        
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