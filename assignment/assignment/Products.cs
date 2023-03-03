using SQLite;

namespace assignment
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } // Auto-incrementing integer ID
        public string ProductName { get; set; } // Name of the product
        public string Description { get; set; } // Description of the product
        public double Price { get; set; } // Price of the product

        private int _NumInteractions = 0;
        public int NumInteractions { get; set; }
    }
}