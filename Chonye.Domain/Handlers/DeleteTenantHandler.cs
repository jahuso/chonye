using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class DeleteTenantHandler : IRequestHandler<DeleteTenantRequest, IDeleteTenantResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;
        public DeleteTenantHandler(ChonyeContext context, ILogger<DeleteTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IDeleteTenantResponse> Handle(DeleteTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants
                .Where(t => t.GlobalId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (tenant == null)
            {
                return new IDeleteTenantResponse.NotFound();
            }

            try
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync(cancellationToken);
                return new IDeleteTenantResponse.Succeeded(request.Id);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant delete has failed with message: {Message}", errorMessage);
                return new IDeleteTenantResponse.Exception();
            }
        }
    }
}
