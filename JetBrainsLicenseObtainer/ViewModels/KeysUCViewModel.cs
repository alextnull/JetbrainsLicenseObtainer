using JetBrainsLicenseObtainer.Infrastructure;
using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.ViewModels.Base;

namespace JetBrainsLicenseObtainer.ViewModels
{
    public class KeysUCViewModel : ViewModelBase
    {
        #region Keys Collection

        AsyncObservableCollection<Key> _keys;

        public AsyncObservableCollection<Key> Keys
        {
            get => _keys;
            set => Set(ref _keys, value);
        }

        #endregion

        #region Constructor

        public KeysUCViewModel()
        {

        }

        #endregion
    }
}
