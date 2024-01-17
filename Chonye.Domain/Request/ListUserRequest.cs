using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class ListUserRequest : IRequest<IListUserResponse>
    {
        public Guid UserId;
    }
}
