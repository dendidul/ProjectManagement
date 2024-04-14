using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Config
{
    public interface IConfigCreatorHelper
    {
        string Get(string configKey);
        string GetConnectionString(string connectionName);
        int GetInteger(string configKey);
        double GetDouble(string configKey);
        bool GetBoolean(string configKey);
        byte[] GetByte(string configKey);
    }
}
