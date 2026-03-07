using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellCroissantDesktop
{
    public class QuantityBasedRuleDetail
    {
        public int RuleId { get; set; }

        public int? PromotionId { get; set; }

        public int MinQuantity { get; set; }

        public decimal DiscountValue { get; set; }
    }
}
