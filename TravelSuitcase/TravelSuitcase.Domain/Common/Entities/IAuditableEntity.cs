namespace TravelSuitcase.Domain.Common.Entities
{
    public interface IAuditableEntity : IEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public int? LastModifiedBy { get; set; }
    }
}