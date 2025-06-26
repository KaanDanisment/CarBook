using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Abstract
{
    public interface ITransactionService
    {
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken);
    }
}
