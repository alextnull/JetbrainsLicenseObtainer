using JetBrainsLicenseObtainer.Infrastructure.Commands;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System.Diagnostics;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.ViewModels
{
    public class AboutUCViewModel : ViewModelBase
    {
        #region Commands

        #region OpenGithubPageCommand

        public ICommand OpenGithubPageCommand { get; set; }

        private bool CanOpenGithubPageCommandExecute(object parameter) => true;
        private void OnOpenGithubPageCommandExecuted(object parameter)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "https://github.com/alextnull/JetBrainsLicenseObtainer",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        #endregion

        #endregion

        #region Constructor

        public AboutUCViewModel()
        {
            #region Commands

            OpenGithubPageCommand = new RelayCommand(OnOpenGithubPageCommandExecuted, CanOpenGithubPageCommandExecute);

            #endregion
        }

        #endregion
    }
}
