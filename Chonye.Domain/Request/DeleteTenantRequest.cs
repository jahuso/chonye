using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class DeleteTenantRequest :IRequest<IDeleteTenantResponse>
    {
        public DeleteTenantRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
