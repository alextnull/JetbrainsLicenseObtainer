using JetBrainsLicenseObtainer.Models;
using JetBrainsLicenseObtainer.ViewModels.Base;
using System;
using System.Collections.ObjectModel;

namespace JetBrainsLicenseObtainer.ViewModels
{
    class AccountsUCViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }

        public AccountsUCViewModel()
        {
            //Just for checking, remove it later
            Accounts = new ObservableCollection<Account>
            {
                new Account("Oleg T", "oleg@gmail.com", "12345", DateTime.Now),
                new Account("Masha D", "masha@gmail.com", "12345", DateTime.Now),
                new Account("Dima G", "dimag344@gmail.com", "12345", DateTime.Now)
            };
        }
    }
}
