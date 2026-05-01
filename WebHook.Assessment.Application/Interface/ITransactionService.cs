using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHook.Assessment.Application.DTO;
using WebHook.Assessment.Application.Models;

namespace WebHook.Assessment.Application.Interface
{
    public interface ITransactionService
    {
 
        Task<ServerResponse<DerivedResult>> ProcessWebHookAsync(TransactionDto request);
    }
}
