using Microsoft.EntityFrameworkCore;

namespace Chonye.Domain.Helpers
{
    public interface IErrorHandlingService<T> where T : DbContext
    {
        ServiceError ServiceError(int code, params object[] args);
    }
}
