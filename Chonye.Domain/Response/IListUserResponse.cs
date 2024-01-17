using Chonye.Domain.Helpers;
using Chonye.Domain.Models;

namespace Chonye.Domain.Response
{
    public interface IListUserResponse: DomainResponse
    {
        public record Succeeded(IQueryable<Tenant> Items) : IListUserResponse, Success;
        public record InvalidData(ServiceError Error) : IListUserResponse, Failure;
        public record NotFound(ServiceError Error) : IListUserResponse, Failure;
        public record InternalError(string Message) : IListUserResponse, Failure;
    }
}
