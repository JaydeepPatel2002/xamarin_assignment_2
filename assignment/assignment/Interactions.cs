using System;
using SQLite;

namespace assignment
{
    public class Interactions
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } // Auto-incrementing integer ID
        public int CustomerID { get; set; } // Customer ID - refers to the associated customer
        public DateTime Date { get; set; } // Date of the interaction
        public string Comments { get; set; } // Comments about the interaction
        public int ProductID { get; set; } // Product ID - refers to the associated product
        public bool Purchased { get; set; } // Indicates whether the customer purchased the product or not
    }
}