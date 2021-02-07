using Dapper;
using JetBrainsLicenseObtainer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace JetBrainsLicenseObtainer.Data
{
    public class KeysDataAccess : SqliteDataAccess
    {
        public static List<Key> LoadKeys()
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                IEnumerable<Key> keys = db.Query<Key>("select * from Keys", new DynamicParameters());
                return keys.ToList();
            }
        }

        public static void SaveKey(Key key)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                db.Execute("insert into Keys (LicenseKey, ExpirationDate, Account)" +
                            "values (@LicenseKey, @ExpirationDate, @Account)", key);
            }
        }

        public static void RemoveKey(Key key)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                db.Execute("delete from Keys where Id = '@Id'", key);
            }
        }
    }
}
