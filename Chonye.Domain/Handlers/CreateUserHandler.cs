using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class CreateUserHandler: IRequestHandler<CreateUserRequest, ICreateUserResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;

        public CreateUserHandler(ChonyeContext context, ILogger<CreateUserHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ICreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            };

            try
            {
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new ICreateUserResponse.Succeeded(user.GlobalId);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("User creation has failed with message: {Message}", errorMessage);
                return new ICreateUserResponse.InternalError(errorMessage);
            }
        }
    }
}
