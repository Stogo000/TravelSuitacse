using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Domain.ValueObjects
{
    public class SuitCaseItem : Entity
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int SuitCaseId { get; set; }
        public SuitCase SuitCase { get; set; }
    }
}