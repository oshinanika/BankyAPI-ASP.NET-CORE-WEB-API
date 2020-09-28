using BankyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankyAPI.Repository.IRepository
{
    public interface IBankRepository
    {
        ICollection<Bank> GetAccounts();
        Bank GetAccount(int Id);
        bool AccountExists(string AccountNumber);
        bool AccountExists(int Id);
        bool CreateAccount(Bank bank);
        bool UpdateAccount(Bank bank);
        bool DeleteAccount(Bank bank);

        bool Save();


    }
}
