using JetBrainsLicenseObtainer.Data;
using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.Services;
using JetBrainsLicenseObtainer.Services.CsvExport;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region Selected key

        Models.Key _selectedKey;

        public Models.Key SelectedKey
        {
            get => _selectedKey;
            set => Set(ref _selectedKey, value);
        }

        #endregion

        #region Commands

        #region LoadKeysCommand

        ICommand LoadKeysCommand { get; set; }

        private bool CanLoadKeysCommandExecute(object parameter) => true;
        private void OnLoadKeysCommandExecuted(object parameter)
        {
            using (DataContext db = new DataContext())
            {
                Keys = new AsyncObservableCollection<Models.Key>(db.Keys.ToList());
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
                Export.ToCsv<Models.Key, KeyMap>(db.Keys.ToList());
            }
        }

        #endregion

        #region OrganizeKeysCommand

        public ICommand OrganizeKeysCommand { get; set; }

        private bool CanOrganizeKeysCommandExecute(object parameter) => true;
        private void OnOrganizeKeysCommandExecuted(object parameter)
        {
            List<Models.Key> keys = new List<Models.Key>();
            using (DataContext db = new DataContext())
            {
                keys = db.Keys.ToList();
            }

            foreach (Models.Key key in keys)
            {
                bool isKeyValid = DateTime.Now > key.ExpirationDate;
                if (isKeyValid)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.OutdatedKeys.Add(new Models.OutdatedKey(key));
                        db.Keys.Remove(key);
                        db.SaveChanges();
                    }
                }
            }

        }

        #endregion

        #region SendToOutdatedKeysCommand

        public ICommand SendToOutdatedKeysCommand { get; set; }

        private bool CanSendToOutdatedKeysCommandExecute(object parameter) => true;
        private void OnSendToOutdatedKeysCommandExecuted(object parameter)
        {
            if (SelectedKey != null)
            {
                using (DataContext db = new DataContext())
                {
                    db.OutdatedKeys.Add(new Models.OutdatedKey(SelectedKey));
                    db.Keys.Remove(SelectedKey);
                    db.SaveChanges();
                }
                LoadKeysCommand.Execute(null);
            }
        }

        #endregion

        #endregion

        #region Constructor

        public KeysUCViewModel()
        {
            #region Commands

            LoadKeysCommand = new RelayCommand(OnLoadKeysCommandExecuted, CanLoadKeysCommandExecute);
            ExportToCsvCommand = new RelayCommand(OnExportToCsvCommandExecuted, CanExportToCsvCommandExecute);
            OrganizeKeysCommand = new RelayCommand(OnOrganizeKeysCommandExecuted, CanOrganizeKeysCommandExecute);
            SendToOutdatedKeysCommand = new RelayCommand(OnSendToOutdatedKeysCommandExecuted, CanSendToOutdatedKeysCommandExecute);

            #endregion

            OrganizeKeysCommand.Execute(null);
            LoadKeysCommand.Execute(null);
            
        }

        #endregion
    }
}
