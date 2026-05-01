using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHook.Assessment.Application.DTO
{
   

    public class TransactionDto
    {
        [Required]
        public string ExternalId { get; set; } = default!;
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; } = default!; // ISO code (e.g., USD)
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
