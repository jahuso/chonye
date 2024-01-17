using System.Net;

namespace Chonye.Domain.Helpers
{
    public class ServiceResult
    {
        public Guid? Id;

        public ServiceError? Error;

        public object? Custom;

        public HttpStatusCode? StatusCode;
    }
}
