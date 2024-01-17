namespace Chonye.Core.DTOs
{
    public  class TenantDTO
    {
        public int TenantId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public Guid? GlobalId { get; set; }

    }
}
