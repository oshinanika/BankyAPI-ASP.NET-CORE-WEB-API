using BankyAPI.Data;
using BankyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankyAPI.Repository.IRepository
{

    public class BankRepository : IBankRepository
    {
        private readonly BankyDbContext _db;

        public BankRepository(BankyDbContext db)
        {
            _db = db; 
        }

        public ICollection<Bank> GetAccounts()
        {
            try
            {
                return _db.Bank.OrderBy(a => a.AccountHolderName).ToList();
            }
            catch (Exception e)
            {

                throw new Exception("Get Accounts Error :", e);
            }
          }

        public Bank GetAccount(int Id)
        {
            try
            {
                return _db.Bank.FirstOrDefault(a => a.Id == Id);
            }
            catch (Exception e)
            {

                throw new Exception("Get Account Error :", e);
            }
        }


        public bool AccountExists(string AccountNumber)
        {
            try
            {
                bool existsOrNot = _db.Bank.Any(a => a.AccountNumber.ToLower().Trim() == AccountNumber.ToLower().Trim());
                return existsOrNot;
            }
            catch (Exception e)
            {

                throw new Exception("Account Exist by Number Error :", e);
            }
        }

        public bool AccountExists(int Id)
        {
            try
            {
                bool existsOrNot = _db.Bank.Any(a => a.Id == Id);
                return existsOrNot;
            }
            catch (Exception e)
            {

                throw new Exception("Account Exist by Id Error :", e);
            }
        }

        public bool CreateAccount(Bank bank)
        {
            try
            {
                _db.Bank.Add(bank);
                return Save();
            }
            catch (Exception e)
            {

                throw new Exception("Create Account Error :", e);
            }

        }

        public bool UpdateAccount(Bank bank)
        {
            try
            {
                _db.Bank.Update(bank);
                return Save();
            }
            catch (Exception e)
            {

                throw new Exception("Update Account Error :", e);
            }
        }

        public bool DeleteAccount(Bank bank)
        {
            try
            {
                _db.Bank.Remove(bank);
                return Save();
            }
            catch (Exception e)
            {

                throw new Exception("Delete Account Error :", e);
            }
        }



        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception e)
            {

                throw new Exception("Save Changes  Error :", e);
            }
        }


    }
}
