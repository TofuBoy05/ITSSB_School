using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellCroissantDesktop
{
    public class LoyaltyProgram
    {
        public int CustomerId { get; set; }

        public int Points { get; set; }

        public string MembershipTier { get; set; } = null!;
    }
}
