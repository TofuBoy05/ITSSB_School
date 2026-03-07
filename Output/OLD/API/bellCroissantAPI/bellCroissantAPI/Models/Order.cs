using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace bellCroissantAPI.Models;

public partial class Order
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

    [JsonIgnore]
    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
