using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Infrastructure.Persistence.Repositories.SuitCases
{
    public class SuitCaseRepository : MainRepository<SuitCase>
    {
        public SuitCaseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}