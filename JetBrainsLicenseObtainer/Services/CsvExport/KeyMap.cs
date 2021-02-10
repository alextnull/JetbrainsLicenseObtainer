using CsvHelper.Configuration;
using JetBrainsLicenseObtainer.Models;

namespace JetBrainsLicenseObtainer.Services.CsvExport
{
    public class KeyMap : ClassMap<Key>
    {
        public KeyMap()
        {
            Map(m => m.LicenseKey).Index(0).Name("Jetbrains license key");
            Map(m => m.RegistrationDate).Index(1).Name("Registartion date");
            Map(m => m.ExpirationDate).Index(2).Name("Expiration date");
        }
    }
}
