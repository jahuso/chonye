using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class PatchTenantHandler : IRequestHandler<PatchTenantRequest, IPatchTenantResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;
        public PatchTenantHandler(ChonyeContext context, ILogger<PatchTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IPatchTenantResponse> Handle(PatchTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants
                    .FirstOrDefaultAsync(t => t.GlobalId == request.TenantId, cancellationToken);                                    
            
            if (tenant == null){return new IPatchTenantResponse.TenantNotFound();}

            tenant.Name = request.Name;
            tenant.Address = request.Address;
            tenant.Phone = request.Phone;
            tenant.Email = request.Email;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return new IPatchTenantResponse.Succeeded(request.TenantId);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant update has failed with message: {Message}", errorMessage);
                return new IPatchTenantResponse.InternalError();
            }
        }
    }
}
