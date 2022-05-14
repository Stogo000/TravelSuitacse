using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.Item;

namespace TravelSuitcase.Domain.ValueObjects
{
    public class Item : Entity
    {
        public string Name { get; }
        public int Quantity { get; }
        public bool IsPacked { get; init; } = false;
        public ICollection<SuitCaseItem> SuitCase { get; set; } = new HashSet<SuitCaseItem>();

        public Item(int id, string name, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IllegalOperationException<ItemExceptions>(ItemExceptions.CannotBeEmpty);
            }
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }
}