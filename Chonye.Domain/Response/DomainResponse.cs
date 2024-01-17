using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Response
{
    public interface DomainResponse
    {
        public interface Success : DomainResponse
        {
        }

        public interface Failure : DomainResponse
        {
        }
    }
}
