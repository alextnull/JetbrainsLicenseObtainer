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

        #region Commands

        #region RegistrateStepikAccountCommand
        public ICommand RegistrateStepikAccountCommand { get; set; }

        private bool CanRegistrateStepikAccountCommandExecute(object parameter) => true;
        private void OnRegistrateStepikAccountCommandExecuted(object parameter)
        {
            Stepik stepik = new Stepik();
            Account account = stepik.RegistrateAccount();

            if (account != null)
                Accounts.Add(account);
        }

        #endregion

        #endregion

        public AccountsUCViewModel()
        {
            Accounts = new ObservableCollection<Account>();

            #region Commands

            RegistrateStepikAccountCommand = new RelayCommand(OnRegistrateStepikAccountCommandExecuted, CanRegistrateStepikAccountCommandExecute);

            #endregion
        }
    }
}
