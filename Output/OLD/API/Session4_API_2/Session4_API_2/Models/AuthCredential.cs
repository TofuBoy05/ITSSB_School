using System;
using System.Collections.Generic;

namespace Session4_API_2.Models;

public partial class AuthCredential
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string SecurityQuestion { get; set; } = null!;

    public string SecurityAnswer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
