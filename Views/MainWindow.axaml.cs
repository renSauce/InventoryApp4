using Avalonia.Controls;
using Avalonia.Interactivity;
using InventoryApp.Models;
using System; 

namespace InventoryApp.Views
{
public partial class MainWindow : Window
{
    public OrderBook OrderBook { get; private set; }

    public MainWindow()
    {
        InitializeComponent();

        // 1) Build inventory
        var inventory = new Inventory();
        var pump   = new UnitItem("Hydraulic Pump", 1200m);
        var plc    = new UnitItem("PLC Module", 850m);
        var servo  = new UnitItem("Servo Motor", 950m);


        inventory.Set(pump, 10);
        inventory.Set(plc, 6);
        inventory.Set(servo, 4);


        OrderBook = new OrderBook(inventory);

        var c1 = new Customer("Acme");
        c1.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-5), [
            new OrderLine(pump, 1),

        ]));

        var c2 = new Customer("Nordic");
        c2.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-3), [
            new OrderLine(plc, 2),
        ]));

        var c3 = new Customer("Electro");
        c3.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-2), [
            new OrderLine(servo, 1),
            new OrderLine(plc, 1),
        ]));
        DataContext = this;
        RevenueText.Text = $"{OrderBook.TotalRevenue():C}";
    }

    public void ProcessNextOrder_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OrderBook.ProcessNextOrder())
            RevenueText.Text = $"{OrderBook.TotalRevenue():C}";
    }
}
}
