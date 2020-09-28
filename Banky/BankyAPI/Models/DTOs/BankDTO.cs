using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankyAPI.Models.DTOs
{
    public class BankDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public DateTime AccountCreated { get; set; }
         
    }
}
