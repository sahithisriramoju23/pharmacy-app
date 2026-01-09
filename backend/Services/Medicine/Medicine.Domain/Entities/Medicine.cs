using Medicine.Domain.ValueObjects;

namespace Medicine.Domain.Entities;

public class Medicine
{
    public MedicineId Id { get; private set; } 
    public string Name { get; private set; }
    public DateOnly ExpiryDate { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string Brand { get; private set; }
    public string? Notes { get; private set; }

    public Medicine()
    {
        //for EF Core
    }

    private Medicine(MedicineId id, string name, DateOnly expiryDate, int quantity, decimal price, string brand, string? notes)
    {
        Id = id;
        Name = name;
        ExpiryDate = expiryDate;
        Quantity = quantity;
        Price = price;
        Brand = brand;
        Notes = notes;
    }
    public static Medicine Create(string name, DateOnly expiryDate, int quantity, decimal price, string brand,string? notes = default)
    {
        return new(MedicineId.Create(), name, expiryDate, quantity, price,brand,notes);
    }
    public static Medicine Update(MedicineId id, string name, DateOnly expiryDate, int quantity, decimal price, string brand, string? notes = default)
    {
        return new(id, name, expiryDate, quantity, price, brand, notes);
    }
}
