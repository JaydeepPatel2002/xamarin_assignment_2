﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace assignment
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new customerListPage();
            //NewUI();
        }

        private void NewUI()
        {
            database = Database;
            
            StackLayout layout = new StackLayout();
            List<Products> list = database.GetAllProducts();
            List<Customers> list2 = database.GetAllCustomers();
            
            Button btnNew = new Button { Text = "experiment" };
            Button btnNew2 = new Button { Text = "experiment" };
            
            btnNew.Clicked += (sender, e)=>
            {
                
                Toast.MakeText(Android.App.Application.Context, string.Join(", ", list.Select(x => x.ProductName)), ToastLength.Short)?.Show();
                
            };
            layout.Children.Add(btnNew);
            
            btnNew2.Clicked += (sender, e)=>
            {
                
                Toast.MakeText(Android.App.Application.Context, string.Join(", ", list2.Select(x => x.FirstName)), ToastLength.Short)?.Show();
                
            };
            layout.Children.Add(btnNew2);
            
            MainPage = new ContentPage
            {
                Content = layout
            };

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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
    
    
    
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
    
}