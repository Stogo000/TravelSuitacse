namespace TravelSuitcase.Domain.Common.Validators.Interfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T value);
    }
}