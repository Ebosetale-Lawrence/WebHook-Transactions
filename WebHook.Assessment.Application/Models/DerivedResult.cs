using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Application.Models
{
   

    public class DerivedResult { 
        
        public long TransactionId { get; set; }

        public decimal Fee { get; set; }

        public decimal NetAmount { get; set; }


    }

    public class TransactionSettings
    {
        public decimal FeePercentage { get; set; }
    }
}
