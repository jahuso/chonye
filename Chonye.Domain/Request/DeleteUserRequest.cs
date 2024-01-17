using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class DeleteUserRequest: IRequest<IDeleteUserResponse>
    {
        public DeleteUserRequest(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
