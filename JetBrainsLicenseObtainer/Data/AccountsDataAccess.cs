using Dapper;
using JetBrainsLicenseObtainer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace JetBrainsLicenseObtainer.Data
{
    public class AccountsDataAccess : SqliteDataAccess
    {
        public static List<Account> LoadAccounts()
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                IEnumerable<Account> accounts = db.Query<Account>("select * from Accounts", new DynamicParameters());
                return accounts.ToList();
            }
        }

        public static void SaveAccount(Account account)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                db.Execute("insert into Accounts (FullName, Email, Password, RegistrationDate) " +
                            "values (@FullName, @Email, @Password, @RegistrationDate)", account);
            }
        }

        public static void RemoveAccount(Account account)
        {
            using (IDbConnection db = new SQLiteConnection(_connectionString))
            {
                db.Execute("delete from Accounts where Id = '@Id'", account);
            }
        }
    }
}
