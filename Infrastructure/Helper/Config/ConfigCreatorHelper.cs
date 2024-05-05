using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Config
{
    public  class ConfigCreatorHelper: IConfigCreatorHelper
    {
        private IConfiguration _configuration;

        public ConfigCreatorHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string configKey)
        {
            return _configuration[configKey];
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }

        public int GetInteger(string configKey)
        {
            var intVal = 1;
            int.TryParse(_configuration[configKey], out intVal);
            return intVal;
        }

        public double GetDouble(string configKey)
        {
            double intVal = 0;
            double.TryParse(_configuration[configKey], out intVal);
            return intVal;
        }

        public bool GetBoolean(string configKey)
        {
            bool boolVal = false;
            bool.TryParse(_configuration[configKey], out boolVal);
            return boolVal;
        }

        public byte[] GetByte(string configKey)
        {
            var str = _configuration[configKey];

            return str.Split(',').Select(s => Convert.ToByte(s, 16)).ToArray();
        }


    }
}
