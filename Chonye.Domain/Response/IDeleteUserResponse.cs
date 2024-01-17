using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface IDeleteUserResponse
    {
        public record Succeeded(Guid GlobalId) : IDeleteUserResponse, Success;

        public record NotFound : IDeleteUserResponse, Failure;

        public record Exception : IDeleteUserResponse, Failure;

        public record InternalError(string Message = "Internal error") : IDeleteUserResponse, Failure;
    }
}
