using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.ViewModels.Base;
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
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        public ICommand UpdateViewCommand { get; set; }

        private bool CanUpdateViewCommandExecute(object parameter) => true;
        private void OnUpdateViewCommandExecuted(object parameter)
        {
            if (parameter.ToString() == "Keys")
            {
                SelectedViewModel = new KeysUCViewModel();
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            #region Commands

            UpdateViewCommand = new RelayCommand(OnUpdateViewCommandExecuted, CanUpdateViewCommandExecute);

            #endregion
        }
    }
}
