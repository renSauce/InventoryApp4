
namespace InventoryApp.Models
{
    public sealed class Inventory
    {
        private readonly Dictionary<Item, double> _stock = [];
        public void Set(Item item, double quantity) => _stock[item] = quantity;

        public bool CanFulfill(Order order) =>
            order.OrderLines.All(ol => _stock.TryGetValue(ol.Item, out var have) && have >= ol.Quantity);

        public void Deduct(Order order)
        {
            foreach (var ol in order.OrderLines)
                if (_stock.TryGetValue(ol.Item, out var have))
                    _stock[ol.Item] = have - ol.Quantity;
        }
    }
}
