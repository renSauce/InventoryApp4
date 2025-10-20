namespace InventoryApp.Models

{
    public sealed class OrderLine
{
    public Item Item { get; }
    public double Quantity { get; }
    public decimal LineTotal => (decimal)Quantity * Item.PricePerUnit;
    public OrderLine(Item item, double quantity) { Item = item; Quantity = quantity; }
    public override string ToString() => $"{Item.Name} x {Quantity}";
}

public sealed class Order
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Time { get; }
    public IReadOnlyList<OrderLine> OrderLines { get; }
    public Order(DateTime time, IEnumerable<OrderLine> lines)
    { Time = time; OrderLines = lines.ToList(); }

    public decimal TotalPrice() => OrderLines.Sum(l => l.LineTotal);
    public decimal Total => TotalPrice();
    public string Summary => string.Join(", ",
      OrderLines.Select(ol => $"{ol.Item.Name} x {ol.Quantity}"));
}
}
