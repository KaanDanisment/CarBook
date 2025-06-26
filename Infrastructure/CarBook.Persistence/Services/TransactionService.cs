using CarBook.Application.Services.Abstract;
using CarBook.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly CarBookContext _context;

        public TransactionService(CarBookContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
