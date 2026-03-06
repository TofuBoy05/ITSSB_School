using System;
using System.Collections.Generic;

namespace Session4_API_2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual AuthCredential? AuthCredential { get; set; }

    public virtual UserPreference? UserPreference { get; set; }
}
