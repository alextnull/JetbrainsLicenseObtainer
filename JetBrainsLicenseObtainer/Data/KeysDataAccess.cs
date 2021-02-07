using Dapper;
using JetBrainsLicenseObtainer.Models;
using System;
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
                           $"values (@LicenseKey, @ExpirationDate, '{SerializeAccount(key.Account)}')", key);
            }
        }

        public static void RemoveKey(Key key)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                db.Execute("delete from Keys where Id = @Id", key);
            }
        }

        public static string SerializeAccount(Account account)
        {
            return $"{account.Id};{account.FullName};{account.Email};{account.Password};{account.RegistrationDate.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        public static Account DeserializeAccount(string accountString)
        {
            string[] accountData = accountString.Split(";");
            Account account = new Account()
            {
                Id = int.Parse(accountData[0]),
                FullName = accountData[1],
                Email = accountData[2],
                Password = accountData[3],
                RegistrationDate = DateTime.Parse(accountData[4])
            };

            return account;
        }
    }
}
