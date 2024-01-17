using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chonye.Domain.Handlers
{
    public class CreateTenantHandler : IRequestHandler<CreateTenantRequest, ICreateTenantResponse>
    {
        private readonly ChonyeContext _context;
        private readonly ILogger _logger;

        public CreateTenantHandler(ChonyeContext context, ILogger<CreateTenantHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ICreateTenantResponse> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
        { 
            var tenant = new Tenant() 
            { 
                Address = request.Address,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone
            };

            try
            {
                await _context.Tenants.AddAsync(tenant, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new ICreateTenantResponse.Succeeded(tenant.GlobalId);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Tenant creation has failed with message: {Message}", errorMessage);
                return new ICreateTenantResponse.InternalError(errorMessage);
            }
        }

    }
}
