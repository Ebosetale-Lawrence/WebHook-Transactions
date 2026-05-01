using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Domain.Entities
{
   

    public class DerivedTransaction
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public decimal Fee { get; set; }
        public decimal NetAmount { get; set; }
    }
}
