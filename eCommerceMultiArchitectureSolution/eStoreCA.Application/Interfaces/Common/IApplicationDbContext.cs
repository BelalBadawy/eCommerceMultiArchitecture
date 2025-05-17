
using Microsoft.EntityFrameworkCore;
using eStoreCA.Domain.Entities;
using System.Data;

namespace eStoreCA.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        Task StartTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();

        Task<IReadOnlyList<T>> QueryAsync<T>(string customConnectionString, string sql, bool isStoredProcedure, object? param = null, IDbTransaction? transaction = null);

        Task<T> QueryFirstOrDefaultAsync<T>(string customConnectionString, string sql, bool isStoredProcedure, object? param = null, IDbTransaction? transaction = null);
        Task<T> ExecuteReturnScalarAsync<T>(string customConnectionString, string sql, bool isStoredProcedure, object param = null, IDbTransaction? transaction = null);
        void ExecuteWithoutReturnAsync(string customConnectionString, string sql, bool isStoredProcedure, object param = null, IDbTransaction? transaction = null);

        Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, object[] parameters = null, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<ApplicationUser> ApplicationUsers { get; set; }


        DbSet<LogUserActivity> LogUserActivities { get; set; }
        DbSet<AuditTrailLog> AuditTrailLogs { get; set; }
        DbSet<AppClaim> AppClaims { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<Category> Categories { get; set; }
        #region Custom
        #endregion Custom


    }
}
