using Microsoft.Extensions.Configuration;
using System;

namespace PatientDirectory.DataAccess
{
    public class PatientDirectoryConfigManager: IPatientDirectoryConfigManager
    {
        private readonly IConfiguration _configuration;

        public PatientDirectoryConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string PatientDirectoryConnection
        {
            get
            {
                return _configuration["ConnectionStrings:defaultConnection"];
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
    }
}
