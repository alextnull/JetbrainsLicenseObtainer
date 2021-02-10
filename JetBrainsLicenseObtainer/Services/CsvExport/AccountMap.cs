using CsvHelper.Configuration;
using JetBrainsLicenseObtainer.Models;

namespace JetBrainsLicenseObtainer.Services.CsvExport
{
    public class AccountMap : ClassMap<Account>
    {
       public AccountMap()
        {
            Map(m => m.FullName).Index(0).Name("Full name");
            Map(m => m.Email).Index(1).Name("Email");
            Map(m => m.Password).Index(1).Name("Password");
            Map(m => m.RegistrationDate).Index(1).Name("Registration date");
        }
    }
}
