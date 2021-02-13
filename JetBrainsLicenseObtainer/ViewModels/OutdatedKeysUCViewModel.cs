using JetBrainsLicenseObtainer.Data;
using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.Services;
using JetBrainsLicenseObtainer.Services.CsvExport;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System.Linq;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    public class OutdatedKeysUCViewModel : ViewModelBase
    {
        #region Outdated Keys Collection

        AsyncObservableCollection<Models.OutdatedKey> _outdatedKeys;

        public AsyncObservableCollection<Models.OutdatedKey> OutdatedKeys
        {
            get => _outdatedKeys;
            set => Set(ref _outdatedKeys, value);
        }

        #endregion

        #region Selected key

        Models.Key _selectedKey;

        public Models.Key SelectedKey
        {
            get => _selectedKey;
            set => Set(ref _selectedKey, value);
        }

        #endregion

        #region Commands

        #region LoadOutdatedKeysCommand

        public ICommand LoadOutdatedKeysCommand { get; set; }

        private bool CanLoadOutdatedKeysCommandExecute(object parameter) => true;
        private void OnLoadOutdatedKeysCommandExecuted(object parameter)
        {
            using (DataContext db = new DataContext())
            {
                OutdatedKeys = new AsyncObservableCollection<Models.OutdatedKey>(db.OutdatedKeys.ToList());
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
                Export.ToCsv<Models.OutdatedKey, KeyMap>(db.OutdatedKeys.ToList());
            }
        }

        #endregion

        #endregion

        public OutdatedKeysUCViewModel()
        {
            #region Commands

            ExportToCsvCommand = new RelayCommand(OnExportToCsvCommandExecuted, CanExportToCsvCommandExecute);
            LoadOutdatedKeysCommand = new RelayCommand(OnLoadOutdatedKeysCommandExecuted, CanLoadOutdatedKeysCommandExecute);

            #endregion

            LoadOutdatedKeysCommand.Execute(null);
        }
    }
}
