using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHook.Assessment.Application.Interface;
using WebHook.Assessment.Domain.Entities;


namespace WebHook.Assessment.Persistence.DataContexts
{


    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly string _connectionString;
  


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _connectionString = this.Database.GetConnectionString();
        }

        //add db sets

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransactionSummary> TransactionSummarys { get; set; }

        public virtual DbSet<DerivedTransaction> DerivedTransactions { get; set; }

        



        public IDbContextTransaction Begin()
        {
            var trans = this.Database.CurrentTransaction;
            if (this.Database.CurrentTransaction == null)
            {
                trans = this.Database.BeginTransaction();
            }

            return trans;
        }
        public async Task CommitAsync()
        {

            var trans = Begin();

            if (trans != null)
            {

                await trans.CommitAsync();
            }

        }
        public async Task RollbackAsync()
        {
            var trans = Begin();

            if (trans != null)
            {
                await trans.RollbackAsync();
            }

        }
        public DbContext GetAppDbContext()
        {
            return this;
        }

    }
}