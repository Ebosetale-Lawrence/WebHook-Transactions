using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHook.Assessment.Domain.Entities;

namespace WebHook.Assessment.Application.Interface
{
    public interface IAppDbContext
    {
        public  DbSet<Transaction> Transactions { get; set; }

        public  DbSet<TransactionSummary> TransactionSummarys { get; set; }
        public  DbSet<DerivedTransaction> DerivedTransactions { get; set; }


        IDbContextTransaction Begin();
        Task CommitAsync();
        Task RollbackAsync();
        DbContext GetAppDbContext();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();

    }
}
