using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellCroissantDesktop
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Category { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public string? Description { get; set; }

        public bool Seasonal { get; set; }

        public bool Active { get; set; }

        public DateOnly IntroducedDate { get; set; }

        public string? Ingredients { get; set; }

        //public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
