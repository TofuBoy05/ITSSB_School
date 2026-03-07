using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellCroissantDesktop
{
    public class Order
    {
        public int TransactionId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;

        public string Channel { get; set; } = null!;

        public int? StoreId { get; set; }

        public int? PromotionId { get; set; }

        public decimal? DiscountAmount { get; set; }

        public Customer Customer { get; set; } = null!;

        public string CustomerName => $"{Customer.FirstName} {Customer.LastName}";

        public  ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
