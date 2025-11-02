using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class AdminAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }
}
