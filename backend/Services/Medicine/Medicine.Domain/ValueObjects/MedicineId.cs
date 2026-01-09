namespace Medicine.Domain.ValueObjects;

public class MedicineId
{
    public Guid Value { get; private set; }
    public MedicineId()
    {
        //for EF core
    }
    private MedicineId(Guid value)
    {
        Value = value;
    }
    public static MedicineId FromString(string id)
    {
        return new MedicineId(Guid.Parse(id));
    }
    public static MedicineId FromGuid(Guid id)
    {
        return new MedicineId(id);
    }
    public static MedicineId Create()
    {
        return new MedicineId(Guid.NewGuid());
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
