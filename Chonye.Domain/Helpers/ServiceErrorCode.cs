using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Helpers
{
    public enum ServiceErrorCode
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
        REDUNDANT_VALUE,
        UNAUTHORIZED,
        SUSPENDED
    }
}
