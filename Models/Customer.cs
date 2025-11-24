using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Username { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? Motorcycle { get; set; }   
    public string? PlateNumber { get; set; } 

    public virtual CustomerAccount? CustomerAccount { get; set; }


}
