using System;

namespace JetBrainsLicenseObtainer.Models
{
    public class Key
    {
        #region Properties

        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime RegistartionDate { get; set; }

        #endregion

        #region Constructor

        public Key()
        {

        }

        public Key(string licenseKey, DateTime registrationDate, DateTime expirationDate)
        {
            LicenseKey = licenseKey;
            RegistartionDate = registrationDate;
            ExpirationDate = expirationDate;
        }

        #endregion
    }
}
