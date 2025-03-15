using Vestis.Entities;

namespace Vestis._02_Domain.Entities;

public class MeasurementEntryEntity : BaseEntity<Guid>, IEquatable<MeasurementEntryEntity?>
{
    public string Name { get; private set; }
    public double Value { get; private set; }

    public MeasurementEntryEntity(string name, double value)
    {
        Name = name;
        Value = value;
    }

    //Constructor for EF
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
        return other is not null &&
               DeletedDate == other.DeletedDate &&
               Name == other.Name &&
               Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DeletedDate, Name, Value);
    }

    public static bool operator ==(MeasurementEntryEntity? left, MeasurementEntryEntity? right)
    {
        return EqualityComparer<MeasurementEntryEntity>.Default.Equals(left, right);
    }

    public static bool operator !=(MeasurementEntryEntity? left, MeasurementEntryEntity? right)
    {
        return !(left == right);
    }
}