using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class PatchUserHandler : IRequestHandler<PatchUserRequest, IPatchUserResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;
        public PatchUserHandler(ChonyeContext context, ILogger<PatchTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IPatchUserResponse> Handle(PatchUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.GlobalId == request.UserId, cancellationToken);

            if (user == null) { return new IPatchUserResponse.UserNotFound(); }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return new IPatchUserResponse.Succeeded(request.UserId);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant update has failed with message: {Message}", errorMessage);
                return new IPatchUserResponse.InternalError();
            }
        }
    }
}
