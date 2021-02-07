using Dapper;
using JetBrainsLicenseObtainer.Models;
namespace JetBrainsLicenseObtainer.Data
{
    public class SqliteDataAccess
    {
        protected static string _connectionString = @"AppDomain.CurrentDomain.BaseDirectory\jblo.db";
    }
}
