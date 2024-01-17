using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class GetTenantRequest : IRequest<IGetTenantResponse>
    {
        public GetTenantRequest(Guid tenantId)
        {
            TenantId = tenantId;
        }
        public Guid TenantId { get;}
    }
}
