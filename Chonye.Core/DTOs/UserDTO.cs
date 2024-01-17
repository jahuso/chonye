using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Core.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Guid? GlobalId { get; set; }
    }
}
