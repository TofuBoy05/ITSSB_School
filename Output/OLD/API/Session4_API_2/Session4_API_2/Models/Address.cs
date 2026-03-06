using System;
using System.Collections.Generic;

namespace Session4_API_2.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
