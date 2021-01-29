using JetBrainsLicenseObtainer.ViewModels.Base;

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
            set => _selectedViewModel = value; 
        }
        #endregion
    }
}
