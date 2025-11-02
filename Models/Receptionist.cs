using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Receptionist
{
    public int ReceptionistId { get; set; }

    public string? Username { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public virtual ReceptionistAccount? ReceptionistAccount { get; set; }
}
