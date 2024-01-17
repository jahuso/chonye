using Chonye.Domain.Helpers;
using Chonye.Domain.Models;

namespace Chonye.Domain.Response
{
    public interface IListTenantResponse : DomainResponse
    {
        public record Succeeded(IQueryable<Tenant> Items) : IListTenantResponse, Success;

        //TODO: remove this exception and evaluate the impact
        public record InvalidData(ServiceError Error) : IListTenantResponse, Failure;
        public record NotFound(ServiceError Error) : IListTenantResponse, Failure;
        public record Exception : IListTenantResponse, Failure;
        public record InternalError(string Message) : IListTenantResponse, Failure;

    }
}
