﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsShop.Core.Domain.Models;
public class Cloth
{
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string Size { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Material { get; set; } = default!;
    public string? Gender { get; set; }
    public string? Type { get; set; }
    public string ImagePath { get; set; } = default!;

    public Product Product { get; set; } = default!;
}

