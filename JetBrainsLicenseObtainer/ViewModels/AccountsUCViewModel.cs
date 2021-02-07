using JetBrainsLicenseObtainer.Data;
using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services.Stepik;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    class AccountsUCViewModel : ViewModelBase
    {
        public AsyncObservableCollection<Account> Accounts { get; set; }

        #region ViewModel Access

        private bool _viewModelAccess = true;
        public bool ViewModelAccess
        {
            get => _viewModelAccess;
            set => Set(ref _viewModelAccess, value);
        }

        #endregion

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

        #region RegistrateStepikAccountCommandAsync
        public ICommand RegistrateStepikAccountCommandAsync { get; set; }

        private bool CanRegistrateStepikAccountCommandAsyncExecute(object parameter) => true;
        private void OnRegistrateStepikAccountCommandAsyncExecuted(object parameter)
        {
            ViewModelAccess = false;
            Stepik stepik = new Stepik();
            Account account;

            for (int i = 0; i < AccountsCount; i++)
            {
                account = stepik.RegistrateAccount();

                if (account != null)
                {
                    AccountsDataAccess.SaveAccount(account);
                    LoadAccountsCommand.Execute(null);
                }
            }

            stepik.CloseDriver();
            ViewModelAccess = true;
        }

        #endregion

        #region LoadAccountsCommand

        public ICommand LoadAccountsCommand { get; set; }

        private bool CanLoadAccountsCommandExecute(object parameter) => true;
        private void OnLoadAccountsCommandExecuted(object parameter)
        {
            Accounts = new AsyncObservableCollection<Account>(AccountsDataAccess.LoadAccounts());
        }

        #endregion

        #endregion

        public AccountsUCViewModel()
        {
            #region Commands

            RegistrateStepikAccountCommandAsync = new AsyncRelayCommand(OnRegistrateStepikAccountCommandAsyncExecuted, CanRegistrateStepikAccountCommandAsyncExecute);
            IncreasePropertyCommand = new RelayCommand(OnIncreasePropertyCommandExecuted, CanIncreasePropertyCommandExecute);
            DecreasePropertyCommand = new RelayCommand(OnDecreasePropertyCommandExecuted, CanDecreasePropertyCommandExecute);
            LoadAccountsCommand = new RelayCommand(OnLoadAccountsCommandExecuted, CanLoadAccountsCommandExecute);
            
            #endregion

            LoadAccountsCommand.Execute(null);
        }

    }
}
