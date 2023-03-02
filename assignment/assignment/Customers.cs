using SQLite;

namespace assignment
{
    public class Customers
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } // Auto-incrementing integer ID
        public string FirstName { get; set; } // First name of customer
        public string LastName { get; set; } // Last name of customer
        public string Address { get; set; } // Address of customer
        public string Phone { get; set; } // Phone number of customer
        public string Email { get; set; } // Email address of customer
    }
}