using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.City;
using TravelSuitcase.Domain.Common.Exceptions.Country;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Domain.ValueObjects
{
    public class Localization : Entity
    {
        public string City { get; }
        public string Country { get; }
        public ICollection<SuitCase> SuitCases { get; set; } = new HashSet<SuitCase>();

        public Localization(int id, string city, string country)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new IllegalOperationException<CityNameException>(CityNameException.CannotBeEmpty);
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new IllegalOperationException<CountryNameException>(CountryNameException.CannotBeEmpty);
            }
            Id = id;
            City = city;
            Country = country;
        }

        public override string ToString()
            => $"{City},{Country}";
    }
}