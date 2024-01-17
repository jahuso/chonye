using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Core.DTOs
{
    public class UsageDTO
    {
        public int UsageId { get; set; }

        public int TenantId { get; set; }

        public DateTime UsageDate { get; set; }

        public string UsageType { get; set; } = null!;

        public string? UsageDetails { get; set; }

        public Guid? GlobalId { get; set; }

    }
}
