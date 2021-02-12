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
            Id = key.Id;
            LicenseKey = key.LicenseKey;
            ExpirationDate = key.ExpirationDate;
            RegistrationDate = key.RegistrationDate;
        }

        #endregion
    }
}
