using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace Chonye.Domain.Helpers
{
    public class ServiceError
    {
        public enum ERROR_CODE
        {
            UNKNOWN,
            DUPLICATE_VALUE,
            INVALID_REQUEST,
            LIMIT_EXCEEDED,
            ACCESS_DENIED,
            NOT_FOUND,
            UNIQUE_KEY_VIOLATION,
            CONSTRAINT_VIOLATION,
            MISSING_REQUIRED_VALUE,
            REDUNDANT_VALUE
        }

        public ServiceErrorCode ErrorCode;

        public string Description;

        public Guid? TrackingId;

        public int? Code;

        public string? Title;

        public int? Status;

        [JsonIgnore]
        public ModelStateDictionary ModelState;

        public ServiceError()
        {
        }

        public ServiceError(ServiceErrorCode error, string description = null, Guid? trackingId = null, int? code = null, int? status = null)
        {
            ErrorCode = error;
            Description = description;
            Code = code;
            Status = status;
            TrackingId = trackingId;
        }

        public ServiceError(ServiceErrorCode error, ModelStateDictionary modelState)
        {
            ErrorCode = error;
            ModelState = modelState;
        }

        public override string ToString()
        {
            if (ModelState == null)
            {
                string[] obj = new string[6]
                {
                    ErrorCode.ToString(),
                    " - ",
                    Description,
                    " (trackingid=",
                    null,
                    null
                };
                Guid? trackingId = TrackingId;
                obj[4] = trackingId.ToString();
                obj[5] = ")";
                return string.Concat(obj);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return System.Text.Json.JsonSerializer.Serialize(ModelState, options);
        }
    }
}
