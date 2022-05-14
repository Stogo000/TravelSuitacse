using MediatR;
using TravelSuitcase.Domain.Repositories.Users;

namespace TravelSuitcase.Application.Queries.Users.GetUsers
{
    public class GetUsersQuery : IRequest<GetUsersResponseDTO>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponseDTO>
    {
        private IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersResponseDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return new GetUsersResponseDTO(await _userRepository.GetAllAsync(cancellationToken));
        }
    }
}