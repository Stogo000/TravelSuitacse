using MediatR;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Managers.Users;

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
        private UserManager _userManager;

        public RegisterUserCommandHandler(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.CreateAsync(new CreateUserDTO(request.Email, request.Login, request.Password));
            return await Task.FromResult(Unit.Value);
        }
    }
}