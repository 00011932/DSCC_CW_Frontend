using System.Text.Json.Serialization;

namespace Frontend_MVC.Models
{
    // Transaction View Model class to work with Transaction object
    public class Transaction
    {
        public int ID { get; set; }

        public int ItemId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int SoldItemsCount { get; set; }
        public Item? Item { get; set; }
    }
}
