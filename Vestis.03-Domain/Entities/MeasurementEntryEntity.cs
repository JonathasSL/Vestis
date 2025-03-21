namespace Vestis._03_Domain.Entities;

public class MeasurementEntryEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public double Value { get; private set; }

    public MeasurementEntryEntity(string name, double value)
    {
        Name = name;
        Value = value;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public MeasurementEntryEntity() { }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");
        else if (Name != name)
        {
            Name = name;
            SetAsUpdated();
        }
    }

    public void ChangeValue(double value)
    {
        if (Value != value)
        {
            Value = value;
            SetAsUpdated();
        }
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MeasurementEntryEntity);
    }

    public bool Equals(MeasurementEntryEntity? other)
    {
        throw new NotImplementedException();
    }
}