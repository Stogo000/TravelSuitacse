using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.ValueObjects;

namespace TravelSuitcase.Domain.Entities
{
    public class SuitCase : AuditableEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; private set; }
        public int LocalizationId { get; set; }
        public Localization Localization { get; private set; }

        public ICollection<SuitCaseItem> Items { get; set; } = new HashSet<SuitCaseItem>();

        public SuitCase(int id, string name, int localizationId, int userId)
        {
            Id = id;
            Name = name;
            LocalizationId = localizationId;
            UserId = userId;
        }
    }
}