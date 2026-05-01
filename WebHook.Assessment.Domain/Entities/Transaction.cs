using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Domain.Entities
{
    public class Transaction
    {
        public long Id { get; set; } 
        public string ExternalId { get; set; } 
        public decimal Amount { get; set; }
        public string Currency { get; set; }
      
        public DateTime DateCreated { get; set; }
    }
}
