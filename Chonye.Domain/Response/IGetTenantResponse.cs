using Chonye.Domain.Helpers;
using Chonye.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Response
{
    public interface IGetTenantResponse : DomainResponse
    {
        public record Succeeded(Tenant Item) : IGetTenantResponse, Success;
        public record NotFound : IGetTenantResponse, Failure;
        public record Exception : IGetTenantResponse, Failure;
        public record InternalError(string Message) : IGetTenantResponse, Failure;


    }
}
