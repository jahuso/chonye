using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class CreateTenantRequest: IRequest<ICreateTenantResponse>
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public CreateTenantRequest(string name, string email, string? address, string? phone)
        {
            Name = name; Email = email; Address = address; Phone = phone;
        }
    }
}
