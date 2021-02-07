using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JetBrainsLicenseObtainer.Infrastructure.Commands.Base
{
    public abstract class AsyncCommand : ICommand
    {
        private readonly ObservableCollection<Task> runningTasks;

        protected AsyncCommand()
        {
            runningTasks = new ObservableCollection<Task>();
            runningTasks.CollectionChanged += OnRunningTasksChanged;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public IEnumerable<Task> RunningTasks
        {
            get => runningTasks;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            Task runningTask = ExecuteAsync(parameter);

            runningTasks.Add(runningTask);

            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }

        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsync(object parameter);

        private void OnRunningTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
