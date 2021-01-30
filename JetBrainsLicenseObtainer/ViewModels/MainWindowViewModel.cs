using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Selected ViewModel

        private ViewModelBase _selectedViewModel = new AccountsUCViewModel();

        /// <summary>Selected ViewModel</summary>
        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set => Set(ref _selectedViewModel, value);
        }

        #endregion

        #region Commands

        #region UpdateViewCommand

        public ICommand UpdateViewCommand { get; set; }

        private bool CanUpdateViewCommandExecute(object parameter) => true;
        private void OnUpdateViewCommandExecuted(object parameter)
        {
            string viewModel = parameter.ToString();

            SelectedViewModel = viewModel switch
            {
                "Accounts" => new AccountsUCViewModel(),
                "Keys" => new KeysUCViewModel(),
                "OutdatedKeys" => new OutdatedKeysUCViewModel(),
                "About" => new AboutUCViewModel(),
                _ => throw new ArgumentException("Wrong ViewModel parameter")
            };
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands

            UpdateViewCommand = new RelayCommand(OnUpdateViewCommandExecuted, CanUpdateViewCommandExecute);

            #endregion
        }
    }
}
