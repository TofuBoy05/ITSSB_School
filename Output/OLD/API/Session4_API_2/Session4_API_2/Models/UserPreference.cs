using System;
using System.Collections.Generic;

namespace Session4_API_2.Models;

public partial class UserPreference
{
    public int UserId { get; set; }

    public bool? MailingListSub { get; set; }

    public string? PreferredDeliveryMethod { get; set; }

    public int? PreferredDeliveryAddressId { get; set;  }

    public virtual User User { get; set; } = null!;
}
