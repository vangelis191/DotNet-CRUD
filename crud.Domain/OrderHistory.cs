using System;
namespace crud.Domain
{
    public class OrderHistory
    {
        public int OrderHistoryID { get; set; }
        public Guid OrderID { get; set; }
        public string Action { get; set; } // e.g., "Created," "Updated," "Deleted"
        public DateTime Timestamp { get; set; }

        // Other properties to capture relevant data about the order history
    }

}

