using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ReceptionistAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? ReceptionistId { get; set; }

    public virtual Receptionist? Receptionist { get; set; }
}
