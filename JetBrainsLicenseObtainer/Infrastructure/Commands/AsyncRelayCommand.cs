using JetBrainsLicenseObtainer.Infrastructure.Commands.Base;
using System;
using System.Threading.Tasks;

namespace JetBrainsLicenseObtainer.Infrastructure.Commands
{
    public class AsyncRelayCommand : AsyncCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public AsyncRelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override async Task ExecuteAsync(object parameter) => await Task.Run(() => _execute(parameter));
    }
}
