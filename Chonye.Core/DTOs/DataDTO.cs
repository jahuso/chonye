using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Core.DTOs
{
    public class DataDTO
    {
        public int DataId { get; set; }

        public int UserId { get; set; }

        public string Data { get; set; } = null!;

        public Guid? GlobalId { get; set; }
    }
}
