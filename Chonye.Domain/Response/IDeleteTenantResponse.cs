using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface IDeleteTenantResponse
    {
        public record Succeeded(Guid GlobalId) : IDeleteTenantResponse, Success;

        public record NotFound : IDeleteTenantResponse, Failure;

        public record Exception : IDeleteTenantResponse, Failure;

        public record InternalError(string Message = "Internal error") : IDeleteTenantResponse, Failure;
    }
}
