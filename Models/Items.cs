
namespace InventoryApp.Models
{
    public enum MeasurementUnit { Piece, Kilogram }

    // Primary constructor on an abstract class
    public abstract class Item(string name, decimal pricePerUnit) : IEquatable<Item>
    {
        public string Name { get; } = name;
        public decimal PricePerUnit { get; } = pricePerUnit;

        public bool Equals(Item? other) =>
            other is not null && GetType() == other.GetType() && Name == other.Name;

        public override bool Equals(object? obj) => Equals(obj as Item);

        public override int GetHashCode() => (Name, GetType()).GetHashCode();

        public override string ToString() => Name;
    }

    // Derived types also with primary constructors
    public sealed class UnitItem(string name, decimal pricePerUnit, double weightKgPerPiece = 0)
        : Item(name, pricePerUnit)
    {
        public double Weight { get; } = weightKgPerPiece;
    }

    public sealed class BulkItem(string name, decimal pricePerUnit, MeasurementUnit unit)
        : Item(name, pricePerUnit)
    {
        public MeasurementUnit MeasurementUnit { get; } = unit;
    }
}
