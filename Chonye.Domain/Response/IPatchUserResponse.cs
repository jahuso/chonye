using Chonye.Domain.Helpers;
using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface IPatchUserResponse
    {
        public record Succeeded(Guid? GlobalId) : IPatchUserResponse;
        public record UserNotFound() : IPatchUserResponse, Failure;
        public record UserException(ServiceError Error) : IPatchUserResponse, Failure;
        public record InvalidData(ServiceError Error) : IPatchUserResponse, Failure;
        public record InternalError(string Message = "Internal error") : IPatchUserResponse, Failure;

    }
}
