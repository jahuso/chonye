using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration config;

        public static void Initialize(IConfiguration configuration)
        {
            config = configuration;
        }
    }
}
