using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class CreateUserRequest: IRequest<ICreateUserResponse>
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; }
        public CreateUserRequest(string name, string email, string password)
        {
            Name = name; Email = email; Password = password;
        }
    }
}
