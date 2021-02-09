using JetBrainsLicenseObtainer.Data;
using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    public class KeysUCViewModel : ViewModelBase
    {
        #region Keys Collection

        AsyncObservableCollection<Models.Key> _keys;

        public AsyncObservableCollection<Models.Key> Keys
        {
            get => _keys;
            set => Set(ref _keys, value);
        }

        #endregion

        #region Commands

        #region LoadKeysCommand

        ICommand LoadKeysCommand { get; set; }

        private bool CanLoadKeysCommandExecute(object parameter) => true;
        private void OnLoadKeysCommandExecuted(object parameter)
        {
            Keys = new AsyncObservableCollection<Models.Key>(KeysDataAccess.LoadKeys());
        }

        #endregion

        #endregion

        #region Constructor

        public KeysUCViewModel()
        {
            #region Commands

            LoadKeysCommand = new RelayCommand(OnLoadKeysCommandExecuted, CanLoadKeysCommandExecute);

            #endregion

            LoadKeysCommand.Execute(null);
        }

        #endregion
    }
}
