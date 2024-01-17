using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class ListTenantRequest : IRequest<IListTenantResponse>
    {
        public Guid TenantId;
    }
}
