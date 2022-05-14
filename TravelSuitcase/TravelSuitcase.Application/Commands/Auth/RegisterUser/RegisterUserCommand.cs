using MediatR;
using TravelSuitcase.Application.Common;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;

namespace TravelSuitcase.Application.Commands.Auth.RegisterUser
{
    public class RegisterUserCommand : IRequest
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.CreateAsync(new CreateUserDTO(request.Email, request.Login, request.Password));
            return await Task.FromResult(Unit.Value);
        }
    }
}