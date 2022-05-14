namespace TravelSuitcase.Domain.Common.Exceptions
{
    public class TravelSuitcaseExcepion : Exception
    {
        public TravelSuitcaseExcepion()
        {
        }

        public TravelSuitcaseExcepion(string message) : base(message)
        {
        }

        public TravelSuitcaseExcepion(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}