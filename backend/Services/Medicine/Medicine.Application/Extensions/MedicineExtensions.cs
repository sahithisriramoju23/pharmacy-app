using Medicine.Application.Dtos;

namespace Medicine.Application.Extensions;

public static class MedicineExtensions
{
    public static MedicineDto ToMedicineDto(Guid Id, string Name, DateOnly ExpiryDate, string Brand, decimal Price, int Quantity, string? notes)
    {
        return new MedicineDto
        {
            Id = Id,
            Name = Name,
            ExpiryDate = ExpiryDate,
            Brand = Brand,
            Price = Price,
            Quantity = Quantity,
            Notes = notes
        };
    }
}
