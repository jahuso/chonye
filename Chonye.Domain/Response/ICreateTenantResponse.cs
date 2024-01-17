using Chonye.Domain.Helpers;
using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface ICreateTenantResponse
    {
        public record Succeeded(Guid? GlobalId) : ICreateTenantResponse;
        public record TenantException(ServiceError Error) : ICreateTenantResponse, Failure;
        public record InvalidData(ServiceError Error) : ICreateTenantResponse, Failure;
        public record InternalError(string Message) : ICreateTenantResponse, Failure;
    }
}
