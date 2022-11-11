using System;
using System.Collections.Generic;
using System.Text;

namespace PatientDirectory.DataAccess
{
    public interface IPatientDirectoryConfigManager
    {
        string PatientDirectoryConnection { get; }

        string GetConnectionString(string connectionName);
    }
}
