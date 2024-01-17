using Chonye.Domain.Response;
using MediatR;

namespace Chonye.Domain.Request
{
    public class PatchTenantRequest: IRequest<IPatchTenantResponse>
    {
        public Guid TenantId { get; }
        public string Name { get; } = null!;

        public string Email { get; } = null!;

        public string? Address { get; }

        public string? Phone { get; }

        public PatchTenantRequest(Guid tenantId, string name, string email, string? address, string? phone)
        {
            TenantId = tenantId; Name = name; Email = email; Address = address; Phone = phone;
        }
    }
}
