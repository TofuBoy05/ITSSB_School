using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellCroissantDesktop
{
    public class Promotion
    {
        public int PromotionId { get; set; }

        public string PromotionName { get; set; } = null!;

        public string DiscountType { get; set; } = null!;

        public decimal DiscountValue { get; set; }

        public string? ApplicableProducts { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public decimal? MinimumOrderValue { get; set; }

        public int Priority { get; set; }

        public string? QuantityBasedRules { get; set; }
    }
}
