using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, IDeleteUserResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;
        public DeleteUserHandler(ChonyeContext context, ILogger<DeleteUserHandler> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IDeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u=>u.GlobalId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new IDeleteUserResponse.NotFound();
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return new IDeleteUserResponse.Succeeded(request.Id);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("User delete has failed with message: {Message}", errorMessage);
                return new IDeleteUserResponse.Exception();
            }
        }
    }
}
