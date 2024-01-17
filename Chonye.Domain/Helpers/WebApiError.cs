using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Helpers
{
    public class WebApiError
    {
        public int Status { get; set; }

        public int? Code { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public WebApiErrorSource Source { get; set; }

        public string Documentation { get; set; }
    }
}
