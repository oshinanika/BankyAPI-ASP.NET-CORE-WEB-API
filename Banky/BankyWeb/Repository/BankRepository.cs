using BankyWeb.Models;
using BankyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankyWeb.Repository
{
    public class BankRepository : Repository<Bank>, IBankRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public BankRepository(IHttpClientFactory clientFactory) : base (clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
