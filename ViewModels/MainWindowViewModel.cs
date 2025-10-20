// ViewModels/MainViewModel.cs
using System;
using System.Linq;
using System.Windows.Input;
using InventoryApp.Models;

namespace InventoryApp.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        public OrderBook OrderBook { get; }
        public Inventory Inventory { get; }
        public ICommand ProcessNextOrderCommand { get; }
        public decimal TotalRevenue => OrderBook.TotalRevenue();

        public MainViewModel()
        {
            // Seed inventory
            Inventory = new Inventory();
            var pump  = new UnitItem("Hydraulic Pump", 1200m, 8.5);
            var plc   = new UnitItem("PLC Module", 850m);
            var servo = new UnitItem("Servo Motor", 950m, 6.2);
            var grease= new BulkItem("Industrial Grease", 12.5m, MeasurementUnit.Kilogram);

            Inventory.Set(pump, 10);
            Inventory.Set(plc,  6);
            Inventory.Set(servo, 4);
            Inventory.Set(grease, 50); // kg

            OrderBook = new OrderBook(Inventory);

            // Predefined queued orders
            var c1 = new Customer("Acme");
            c1.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-5), new[]
            { new OrderLine(pump, 1), new OrderLine(grease, 4.5) }));

            var c2 = new Customer("Nordic");
            c2.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-3), new[]
            { new OrderLine(plc, 2) }));

            var c3 = new Customer("Electro");
            c3.CreateOrder(OrderBook, new Order(DateTime.Now.AddMinutes(-2), new[]
            { new OrderLine(servo, 1), new OrderLine(plc, 1) }));

            ProcessNextOrderCommand = new RelayCommand(_ =>
            {
                if (OrderBook.ProcessNextOrder())
                    Raise(nameof(TotalRevenue));
            });
        }
    }

    public sealed class RelayCommand : ICommand
    {
        private readonly Action<object?> _exec;
        private readonly Func<object?, bool>? _can;
        public RelayCommand(Action<object?> exec, Func<object?, bool>? can = null) { _exec = exec; _can = can; }
        public bool CanExecute(object? p) => _can?.Invoke(p) ?? true;
        public void Execute(object? p) => _exec(p);
        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
