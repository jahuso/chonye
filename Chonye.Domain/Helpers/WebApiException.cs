using System.Net;

namespace Chonye.Domain.Helpers
{
    public class WebApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public Guid? TrackingId { get; set; }

        public List<WebApiError> Errors { get; set; }

        public WebApiException()
        {
        }

        public WebApiException(HttpStatusCode httpStatusCode, List<WebApiError> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
            TrackingId = null;
        }
    }
}
