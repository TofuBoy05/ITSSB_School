using System;
using System.Collections.Generic;
using System.Linq;

namespace BellCroissantDesktop
{
    public class PromotionConflict
    {
        public List<Promotion> ConflictingPromotions { get; set; } = new List<Promotion>();
        public List<string> ConflictingProductIds { get; set; } = new List<string>();
        public DateOnly OverlapStartDate { get; set; }
        public DateOnly OverlapEndDate { get; set; }
        public int SharedPriority { get; set; }

        public string GetSummary()
        {
            var promoNames = string.Join(", ", ConflictingPromotions.Select(p => p.PromotionName));
            var products = string.Join(", ", ConflictingProductIds);
            return $"Promotions: {promoNames}\nProducts: {products}\nOverlap: {OverlapStartDate} to {OverlapEndDate}";
        }
    }
}
