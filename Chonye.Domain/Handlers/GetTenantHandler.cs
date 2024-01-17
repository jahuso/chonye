using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class GetTenantHandler : IRequestHandler<GetTenantRequest, IGetTenantResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;
        public GetTenantHandler(ChonyeContext context, ILogger<GetTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IGetTenantResponse> Handle(GetTenantRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _context.Tenants
                  .Where(t => t.GlobalId == new Guid("0860f4a6-2180-ee11-8907-4c77cb5f4325"))
                  .FirstOrDefaultAsync(cancellationToken);

                return new IGetTenantResponse.Succeeded(tenant);

                //if (tenant == null)
                //{
                //    return new IGetTenantResponse.NotFound();
                //}
                //else
                //{
                //    return new IGetTenantResponse.Succeeded(tenant);
                //}
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant retrieve has failed with message: {Message}", errorMessage);
                return new IGetTenantResponse.Exception();
            }
        }
    }
}
