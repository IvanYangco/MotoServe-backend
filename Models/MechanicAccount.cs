using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MechanicAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? MechanicId { get; set; }

    public virtual Mechanic? Mechanic { get; set; }
}
