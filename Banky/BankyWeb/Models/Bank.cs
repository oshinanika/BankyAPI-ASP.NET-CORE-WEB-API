using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankyWeb.Models
{
    public class Bank
    {
        //This model was crated based on BankyAPI's BankDTO.cs Model

        //Don't need [key] as this will be bindedto the BankyCOREAPI DB
        public int Id { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public byte[] IdentificationImage { get; set; }

        public DateTime AccountCreated { get; set; }
    }
}
