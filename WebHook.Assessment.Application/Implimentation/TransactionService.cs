using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHook.Assessment.Application.DTO;
using WebHook.Assessment.Application.Interface;
using WebHook.Assessment.Application.Models;
using WebHook.Assessment.Domain.Entities;

namespace WebHook.Assessment.Application.Implimentation
{
   
    public class TransactionService : ITransactionService
    {
        private readonly IAppDbContext _IAppDbContext;
        private readonly decimal _feePercentage;
        private IDbContextTransaction _trans;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(IAppDbContext IAppDbContext, IOptions<TransactionSettings> settings, ILogger<TransactionService> logger)
        {
            _IAppDbContext = IAppDbContext;
            _feePercentage = settings.Value.FeePercentage;
            _trans = _IAppDbContext.Begin();
            _logger = logger;
        }

        public async Task<ServerResponse<DerivedResult>> ProcessWebHookAsync(TransactionDto request)
             {




            try
            {
                var response = new ServerResponse<DerivedResult>();

                int save = 0;

                // Idempotency check


                var existing = await _IAppDbContext.Transactions.FirstOrDefaultAsync(t => t.ExternalId == request.ExternalId);

                if (existing != null)
                {

                    _logger.LogInformation("Duplicate transaction not allowed: {ExternalId}", request.ExternalId);

                    response.IsSuccessful = false; ;
                    response.SuccessMessage = "Record (s) already exist";
                    return response;

                }




                var transaction = new Transaction
                {
                    ExternalId = request.ExternalId,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    DateCreated = request.Timestamp
                };


                var fee = CalculateFee(transaction.Amount);

                var derived = new DerivedTransaction
                {
                    Transaction = transaction,
                    Fee = fee,
                    NetAmount = transaction.Amount - fee
                };

                var derivedResult = new DerivedResult
                {
                    TransactionId = transaction.Id,
                    Fee = derived.Fee,
                    NetAmount = derived.NetAmount
                };
                _IAppDbContext.Transactions.AddAsync(transaction);
                _IAppDbContext.DerivedTransactions.AddAsync(derived);


               
                save = await _IAppDbContext.SaveChangesAsync();

                if (save > 0)

                {

                    _logger.LogInformation("Transaction Saved successfully");
                    await _trans.CommitAsync();

                    response.Data = derivedResult;
                    response.IsSuccessful = true;
                    response.SuccessMessage = "Record Submitted sucessfully";
                    return response;


                }
                else
                {
                    _logger.LogInformation("Unable to Saved transaction");
                    await _trans.RollbackAsync();
                    response.Data = null;
                    response.IsSuccessful = false; ;
                    response.SuccessMessage = "Failed";
                    return response;

                }

            
            }
            catch (Exception ex)
            {

               _logger.LogWarning("Error occured", ex.Message);
                return null;

            }
            
            

        }


        private  decimal CalculateFee(decimal amount)
        {
            return Math.Round(amount * _feePercentage, 2);
        }

    }



}



 
