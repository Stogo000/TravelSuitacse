namespace TravelSuitcase.Domain.Common.Entities
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}