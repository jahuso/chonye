using Chonye.Domain.Helpers;
using static Chonye.Domain.Response.DomainResponse;

namespace Chonye.Domain.Response
{
    public interface ICreateUserResponse
    {
        public record Succeeded(Guid? GlobalId) : ICreateUserResponse;
        public record UserException(ServiceError Error) : ICreateUserResponse, Failure;
        public record InvalidData(ServiceError Error) : ICreateUserResponse, Failure;
        public record InternalError(string Message) : ICreateUserResponse, Failure;
    }
}
