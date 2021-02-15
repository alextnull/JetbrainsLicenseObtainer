using System;

namespace JetBrainsLicenseObtainer.Models
{
    public class OutdatedKey : KeyBase
    {
        #region Properties

        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime RegistrationDate { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{LicenseKey} {RegistrationDate.ToString("dd.MM.yyyy HH:mm:ss")} {ExpirationDate.ToString("dd.MM.yyyy HH:mm:ss")}";
        }

        #endregion

        #region Constructor

        public OutdatedKey()
        {

        }

        public OutdatedKey(string licenseKey, DateTime registrationDate, DateTime expirationDate)
        {
            LicenseKey = licenseKey;
            RegistrationDate = registrationDate;
            ExpirationDate = expirationDate;
        }

        public OutdatedKey(Key key)
        {
            LicenseKey = key.LicenseKey;
            ExpirationDate = key.ExpirationDate;
            RegistrationDate = key.RegistrationDate;
        }

        #endregion
    }
}
