using Chonye.Domain.Helpers;
using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface IPatchTenantResponse
    {
        public record Succeeded(Guid? GlobalId) : IPatchTenantResponse;
        public record TenantNotFound() : IPatchTenantResponse, Failure;
        public record TenantException(ServiceError Error) : IPatchTenantResponse, Failure;
        public record InvalidData(ServiceError Error) : IPatchTenantResponse, Failure;
        public record InternalError(string Message = "Internal error") : IPatchTenantResponse, Failure;
    }
}
