
```mermaid
classDiagram
    direction TB
    class Item { <<abstract>> +string Name +decimal PricePerUnit }
    class UnitItem
    class BulkItem { +MeasurementUnit MeasurementUnit }
    Item <|-- UnitItem
    Item <|-- BulkItem

    class OrderLine { +Item Item +double Quantity +decimal LineTotal }
    class Order { +Guid Id +DateTime Time +IReadOnlyList~OrderLine~ OrderLines +decimal Total }
    Order "1" o-- "1..*" OrderLine

    class Inventory { -Dictionary~Item,double~ stock +Set(Item,double) +CanFulfill(Order) +Deduct(Order) }
    class OrderBook { +ObservableCollection~Order~ QueuedOrders +ObservableCollection~Order~ ProcessedOrders +QueueOrder(Order) +ProcessNextOrder() bool +TotalRevenue() decimal }
    class Customer { +string Name +List~Order~ Orders +CreateOrder(OrderBook,Order) }

    OrderBook --> Inventory
    Customer --> Order
