using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    class AccountsUCViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }

        #region Accounts count

        private int _accountsCount = 1;
        public int AccountsCount
        {
            get => _accountsCount;
            set
            {
                if (value >= 1 && value <= int.MaxValue)
                    Set(ref _accountsCount, value);
            }

        }

        #endregion

        #region Keys hours

        private int _keysHours = 1;

        public int KeysHours
        {
            get => _keysHours;
            set
            {
                if (value >= 1 && value <= 4)
                    Set(ref _keysHours, value);
            }

        }

        #endregion

        #region Commands

        #region IncreasePropertyCommand

        public ICommand IncreasePropertyCommand { get; set; }

        private bool CanIncreasePropertyCommandExecute(object parameter) => true;
        private void OnIncreasePropertyCommandExecuted(object parameter)
        {
            switch (parameter)
            {
                case "AccountsCount":
                    AccountsCount++;
                    break;
                case "KeysHours":
                    KeysHours++;
                    break;
            }
        }

        #endregion

        #region DecreasePropertyCommand

        public ICommand DecreasePropertyCommand { get; set; }

        private bool CanDecreasePropertyCommandExecute(object parameter) => true;
        private void OnDecreasePropertyCommandExecuted(object parameter)
        {
            switch (parameter)
            {
                case "AccountsCount":
                    AccountsCount--;
                    break;
                case "KeysHours":
                    KeysHours--;
                    break;
            }
        }

        #endregion

        #region RegistrateStepikAccountCommand
        public ICommand RegistrateStepikAccountCommand { get; set; }

        private bool CanRegistrateStepikAccountCommandExecute(object parameter) => true;
        private void OnRegistrateStepikAccountCommandExecuted(object parameter)
        {
            Stepik stepik = new Stepik();
            Account account;

            for (int i = 0; i < AccountsCount; i++)
            {
                account = stepik.RegistrateAccount();

                if (account != null)
                    Accounts.Add(account);
            }

            stepik.CloseDriver();
        }

        #endregion

        #endregion

        public AccountsUCViewModel()
        {
            Accounts = new ObservableCollection<Account>();

            #region Commands

            RegistrateStepikAccountCommand = new RelayCommand(OnRegistrateStepikAccountCommandExecuted, CanRegistrateStepikAccountCommandExecute);
            IncreasePropertyCommand = new RelayCommand(OnIncreasePropertyCommandExecuted, CanIncreasePropertyCommandExecute);
            DecreasePropertyCommand = new RelayCommand(OnDecreasePropertyCommandExecuted, CanDecreasePropertyCommandExecute);

            #endregion
        }
    }
}
