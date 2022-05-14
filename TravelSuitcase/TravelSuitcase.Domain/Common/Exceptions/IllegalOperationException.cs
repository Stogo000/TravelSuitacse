namespace TravelSuitcase.Domain.Common.Exceptions
{
    public class IllegalOperationException<TException> : TravelSuitcaseExcepion
    {
        public TException Status { get; }

        public IllegalOperationException(TException status)
            : base($"IllegalOperationException occurred: " +
                   $"{typeof(TException)}" +
                   $"-" +
                   $"{Enum.GetName(typeof(TException), Convert.ToInt32(status))}")
        {
            if (typeof(TException) is not { IsEnum: true })
            {
                throw new TravelSuitcaseExcepion("Exception status must be an enum");
            }

            Status = status;
        }
    }
}