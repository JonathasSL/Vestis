using Vestis.Entities;

namespace Vestis._02_Domain.Entities
{
    public class BodyMeasurementEntity : BaseEntity<Guid>
    {
        public DateTime MeasurementDate { get; private set; }
        public List<MeasurementEntryEntity> Entries { get; private set; }

        public BodyMeasurementEntity(DateTime measurementDate)
        {
            MeasurementDate = measurementDate;
        }

        //Constructor for EF
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
}
