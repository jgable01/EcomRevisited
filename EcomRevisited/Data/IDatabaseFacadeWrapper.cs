using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcomRevisited.Data
{
    public interface IDatabaseFacadeWrapper
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }

    public class DatabaseFacadeWrapper : IDatabaseFacadeWrapper
    {
        private readonly DatabaseFacade _databaseFacade;

        public DatabaseFacadeWrapper(DatabaseFacade databaseFacade)
        {
            _databaseFacade = databaseFacade;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _databaseFacade.BeginTransactionAsync();
        }
    }

}
