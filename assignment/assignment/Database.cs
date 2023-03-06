using System;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using SQLite;

namespace assignment
{
    public class Database
    {
        readonly SQLiteConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            //database.DropTable<FuelPurchase>(); // can call this to drop if needed
            database.DropTable<Customers>();
            database.DropTable<Interactions>();
            database.DropTable<Products>();
            database.CreateTable<Customers>(); // won’t do anything if already exists
            database.CreateTable<Interactions>(); // won’t do anything if already exists
            database.CreateTable<Products>(); // won’t do anything if already exists
            if(database.Table<Products>().Count()==0) //if no records  make one
            {
                // this will get the next key
                Products prod1 = new Products();
                Products prod2 = new Products();
                Products prod3 = new Products();
                prod1.ProductName = "Wonder Jacket";
                prod1.Description = "A wonderful jacket";
                prod1.Price = 499.99;
                prod1.NumInteractions = 0;
                
                prod2.ProductName = "Wonder Hat";
                prod2.Description = "A wonderful Hat";
                prod2.Price = 124.99;
                prod1.NumInteractions = 0;

                
                prod3.ProductName = "Wonder Boots";
                prod3.Description = "A wonderful Boots";
                prod3.Price = 224.99;
                prod1.NumInteractions = 0;

                
                SaveProduct(prod1);
                SaveProduct(prod2);
                SaveProduct(prod3);
            }
            
            if(database.Table<Customers>().Count()==0) //if no records  make one
            {
                // this will get the next key
                Customers cust1 = new Customers();
                Customers cust2 = new Customers();
                Customers cust3 = new Customers();
                cust1.FirstName = "Jaydeep";
                cust1.LastName = "Patel";
                cust1.Address = "127, Procter pl";
                cust1.Phone = "6395612345";
                cust1.Email = "Jaydeep@saskpoly.ca";
                
                cust2.FirstName = "Liam";
                cust2.LastName = "Willis";
                cust2.Address = "234, Parliament pl";
                cust2.Phone = "6395612345";
                cust2.Email = "Liam@saskpoly.ca";
                
                cust3.FirstName = "David";
                cust3.LastName = "Williams";
                cust3.Address = "432, Robinson pl";
                cust3.Phone = "6395612345";
                cust3.Email = "David@saskpoly.ca";
                
               
                SaveCustomer(cust1);
                SaveCustomer(cust2);
                SaveCustomer(cust3);
                
            }

            if (database.Table<Interactions>().Count() == 0) //if no records  make one
            {
                Interactions Int1 = new Interactions();
                Interactions Int2 = new Interactions();
                Interactions Int3 = new Interactions();
                Interactions Int4 = new Interactions();
                Interactions Int5 = new Interactions();

                Int1.CustomerID = 1;
                Int1.Date = new DateTime(2018, 1, 15);
                Int1.Comments = "good talk";
                Int1.ProductID = 1;
                Int1.Purchased = false;
                
                Int2.CustomerID = 1;
                Int2.Date = new DateTime(2018, 2, 16);
                Int2.Comments = "NICE talk";
                Int2.ProductID = 2;
                Int2.Purchased = true;
                
                Int3.CustomerID = 1;
                Int3.Date = new DateTime(2018, 3, 17);
                Int3.Comments = "bad talk";
                Int3.ProductID = 3;
                Int3.Purchased = false;
                
                Int4.CustomerID = 2;
                Int4.Date = new DateTime(2018, 4, 18);
                Int4.Comments = "spicy talk";
                Int4.ProductID = 2;
                Int4.Purchased = true;
                
                Int5.CustomerID = 2;
                Int5.Date = new DateTime(2018, 5, 19);
                Int5.Comments = "sweet talk";
                Int5.ProductID = 1;
                Int5.Purchased = false;

                SaveInteraction(Int1);
                SaveInteraction(Int2);
                SaveInteraction(Int3);
                SaveInteraction(Int4);
                SaveInteraction(Int5);


            }


        }

        public void resetDatabase()
        {
            database.DropTable<Customers>();
            database.DropTable<Interactions>();
            
            database.CreateTable<Customers>(); // won’t do anything if already exists
            database.CreateTable<Interactions>(); // won’t do anything if already exists

            InteractionListPage.listView.ItemsSource = null;
            customerListPage.listView.ItemsSource = null;


        }
        
        public int SaveProduct(Products item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        //======================================================================================
        //METHODS for Products
        //======================================================================================
        
        public Products GetOneProduct(int id)
        {
            //return database.Table<Customers>().Where(i => i.ID == id).FirstOrDefault();
            return database.Table<Products>().FirstOrDefault(i => i.ID == id);
        }
        public List<Products> GetAllProducts()
        {
            return database.Table<Products>().ToList<Products>();
        }

        public void UpdateNumInteractionOfProduct(int id)
        {
            Products tmpProd = this.GetOneProduct(id);
            tmpProd.NumInteractions++;
            database.Update(tmpProd);
            Toast.MakeText(Android.App.Application.Context, "updating database -> "+tmpProd.NumInteractions, ToastLength.Short)?.Show();

        }
        
        
        //======================================================================================
        //METHODS for customers
        //======================================================================================
        public int SaveCustomer(Customers item)
        {
            int somthing = 0;
            if (item.ID != 0)
            {
                somthing = database.Update(item);
            }
            else
            {
                somthing = database.Insert(item);
            }
            customerListPage.listView.ItemsSource = null;
            customerListPage.listView.ItemsSource = GetAllCustomers();
            return somthing;


        }
        public int DeleteCustomer(Customers item)
        {
            int somthing = 0;
            if (item == null)
            {
                somthing = -1;
            }
            somthing = database.Delete(item);
            customerListPage.listView.ItemsSource = null;
            customerListPage.listView.ItemsSource = GetAllCustomers();
            return somthing;
        }
        public Customers GetOneCustomer(int id)
        {
            //return database.Table<Customers>().Where(i => i.ID == id).FirstOrDefault();
            return database.Table<Customers>().FirstOrDefault(i => i.ID == id);
        }
        public List<Customers> GetAllCustomers()
        {
            return database.Table<Customers>().ToList<Customers>();
        }
        
        
        //======================================================================================
        //METHODS for Interactions
        //======================================================================================
        
        public int SaveInteraction(Interactions item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                UpdateNumInteractionOfProduct(item.ProductID);
                return database.Insert(item);
            }
        }
        public int DeleteInteractions(Interactions item)
        {
            if (item == null)
            {
                return -1;
            }
            return database.Delete(item);
        }
        public Interactions GetOneInteraction(int id)
        {
            return database.Table<Interactions>().Where(i => i.ID == id).FirstOrDefault();
        }
        public List<Interactions> GetAllInteractions()
        {
            return database.Table<Interactions>().ToList<Interactions>();
        }
        
        public List<Interactions> GetInteractionsOfCustomer(int id)
        {
            return database.Table<Interactions>().Where(i => i.CustomerID == id).ToList();
        }


    }
}