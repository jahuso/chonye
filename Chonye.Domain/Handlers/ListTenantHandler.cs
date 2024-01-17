using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class ListTenantHandler : IRequestHandler<ListTenantRequest, IListTenantResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;

        public ListTenantHandler(ChonyeContext context, ILogger<ListTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IListTenantResponse> Handle(ListTenantRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tenants = from t in _context.Tenants select t;
                return new IListTenantResponse.Succeeded(tenants);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant search has failed with message: {Message}", errorMessage);
                return new IListTenantResponse.InternalError(errorMessage);
            }
        }
    }
}
