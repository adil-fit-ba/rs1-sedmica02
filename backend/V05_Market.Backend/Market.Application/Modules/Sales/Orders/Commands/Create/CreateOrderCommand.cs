﻿namespace Market.Application.Modules.Catalog.Products.Commands.Create;

public class CreateOrderCommand : IRequest<int>
{
    public string? Note { get; set; }

    public List<CreateOrderCommandItem> Items { get; set; } = [];
}

public class CreateOrderCommandItem
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
}