using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Core.DTOs
{
    public class SubscriptionDTO
    {
        public int SubscriptionId { get; set; }

        public int TenantId { get; set; }

        public string SubscriptionType { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid? GlobalId { get; set; }

    }
}
