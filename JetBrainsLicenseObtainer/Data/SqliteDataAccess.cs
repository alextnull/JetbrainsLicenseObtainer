using System;
using System.IO;

namespace JetBrainsLicenseObtainer.Data
{
    public class SqliteDataAccess
    {
        protected static string _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jblo.db");
        protected static string _connectionString = $"Data Source={_databasePath};Version=3";
    }
}
