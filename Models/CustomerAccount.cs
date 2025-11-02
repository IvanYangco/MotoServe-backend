using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CustomerAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
}
