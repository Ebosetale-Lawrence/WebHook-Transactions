using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Domain.Entities
{
    public class TransactionSummary
    {
        public long  Id { get; set; } 
        public DateOnly Date { get; set; }
        public decimal TotalAmount { get; set; }
    }



    
}
