using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class AdminAccount
{
    public int Id { get; set; }

    public string? Email { get; set; } 

    public string? Password { get; set; } 

    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }
}
