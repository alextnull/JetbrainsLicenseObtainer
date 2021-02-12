using JetBrainsLicenseObtainer.Data;
using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.Services;
using JetBrainsLicenseObtainer.Services.CsvExport;
using JetBrainsLicenseObtainer.Services.Stepik;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    public class AccountsUCViewModel : ViewModelBase
    {
        AsyncObservableCollection<Account> _accounts;
        public AsyncObservableCollection<Account> Accounts
        {
            get => _accounts;
            set => Set(ref _accounts, value);
        }

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
                    using (DataContext db = new DataContext())
                    {
                        db.Accounts.Add(account);
                        db.SaveChanges();
                    }
                    LoadAccountsCommand.Execute(null);
                }
            }

            stepik.CloseDriver();
            ViewModelAccess = true;
        }

        #endregion

        #region ParseJetbrainsLicenseCommandAsync
        public ICommand ParseJetbrainsLicenseCommandAsync { get; set; }

        private bool CanParseJetbrainsLicenseCommandAsyncExecute(object parameter) => true;
        private void OnParseJetbrainsLicenseCommandAsyncExecuted(object parameter)
        {
            ViewModelAccess = false;

            List<Account> accounts = new List<Account>();
            using (DataContext db = new DataContext())
            {
                accounts = db.Accounts.ToList();
            }

            Stepik stepik = new Stepik();
            foreach (Account account in accounts)
            {
                TimeSpan timeSinceRegistration = DateTime.Now - account.RegistrationDate;
                bool isAccountRegistratedHourAgo = timeSinceRegistration.TotalHours > 1;

                Models.Key key = null;
                if (isAccountRegistratedHourAgo)
                    key = stepik.ParseKey(account);

                if (key != null)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.Keys.Add(key);
                        db.Accounts.Remove(account);
                        db.SaveChanges();
                    }
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
            using (DataContext db = new DataContext())
            {
                Accounts = new AsyncObservableCollection<Account>(db.Accounts.ToList());
            }
        }

        #endregion

        #region ExportToCsvCommand

        public ICommand ExportToCsvCommand { get; set; }

        private bool CanExportToCsvCommandExecute(object parameter) => true;
        private void OnExportToCsvCommandExecuted(object parameter)
        {
            using (DataContext db = new DataContext())
            {
                Export.ToCsv<Account, AccountMap>(db.Accounts.ToList());
            }
        }

        #endregion

        #region AutoGenerateCommandAsync

        public ICommand AutoGenerateCommandAsync { get; set; }

        private bool CanAutoGenerateCommandAsyncExecute(object parameter) => true;
        private void OnAutoGenerateCommandAsyncExecuted(object parameter)
        {
            RegistrateStepikAccountCommandAsync.Execute(null);
            ViewModelAccess = false;
            Thread.Sleep(TimeSpan.FromHours(KeysHours) + TimeSpan.FromMinutes(7));
            ParseJetbrainsLicenseCommandAsync.Execute(null);
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
            ParseJetbrainsLicenseCommandAsync = new AsyncRelayCommand(OnParseJetbrainsLicenseCommandAsyncExecuted, CanParseJetbrainsLicenseCommandAsyncExecute);
            ExportToCsvCommand = new RelayCommand(OnExportToCsvCommandExecuted, CanExportToCsvCommandExecute);
            AutoGenerateCommandAsync = new AsyncRelayCommand(OnAutoGenerateCommandAsyncExecuted, CanAutoGenerateCommandAsyncExecute);
            #endregion

            LoadAccountsCommand.Execute(null);

        }

    }
}
