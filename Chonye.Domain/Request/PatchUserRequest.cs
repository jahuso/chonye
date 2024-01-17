using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class PatchUserRequest: IRequest<IPatchUserResponse>
    {
        public Guid UserId { get; }
        public string Name { get; } = null!;

        public string Email { get; } = null!;
        public string? Password { get; }

        public PatchUserRequest(Guid userId, string name, string email, string? password)
        {
            UserId = userId; Name = name; Email = email; Password = password;
        }
    }
}
