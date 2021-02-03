using JetBrainsLicenseObtainer.Infrastructure.Commands.Base;
using System;
using System.Threading.Tasks;

namespace JetBrainsLicenseObtainer.Infrastructure.Commands
{
    class AsyncRelayCommand : AsyncCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override async Task ExecuteAsync(object parameter) => await Task.Run(() => _execute(parameter));
    }
}
