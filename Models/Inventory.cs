using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string? Material { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalProfit { get; set; }
}
