namespace Medicine.Application.Dtos;

public class MedicineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
}
