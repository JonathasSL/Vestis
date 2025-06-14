namespace Vestis._03_Domain.Entities;

public class BodyMeasurementEntity : BaseEntity<Guid>
{
    public DateTime MeasurementDate { get; private set; }
    public List<MeasurementEntryEntity> Entries { get; private set; }
    public Guid ProjectId { get; private set; }
    public ProjectEntity Project { get; private set; }

    public BodyMeasurementEntity(DateTime measurementDate)
    {
        MeasurementDate = measurementDate;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public BodyMeasurementEntity() { }

    public void AddEntry(string name, double value)
    {
        Entries ??= new List<MeasurementEntryEntity>();
        Entries.Add(new MeasurementEntryEntity(name,value));
        SetAsUpdated();
    }

    public void RemoveEntry(MeasurementEntryEntity entry)
    {
        if (Entries.Contains(entry))
        {
            entry.SetAsDeleted();
            SetAsUpdated();
        }
    }
}
