﻿namespace RestaurantReservation.Db.Models;

public partial class MenuItem
{
    public int ItemId { get; set; }

    public int ResturantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Resturant Resturant { get; set; } = null!;
}
