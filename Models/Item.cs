using System.ComponentModel.DataAnnotations;

namespace Frontend_MVC.Models
{
    // Item View Model class to work with Item object
    public class Item
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
        public CategoryTypes Category { get; set; }


        public enum CategoryTypes
        {
            Device = 0, furniture = 1, Other = 2
        }
    }
}
