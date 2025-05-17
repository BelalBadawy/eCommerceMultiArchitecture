
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Infrastructure.Common;
using eStoreCA.Infrastructure.Extensions;
using eStoreCA.Shared.Enums;
using eStoreCA.Shared.Interfaces;
using System.Data;
using System.Reflection;

namespace eStoreCA.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private ICurrentUserService _currentUserService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _connectionString;
        private IDbContextTransaction _dbContextTransaction;
        private IConfiguration _configuration;

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTimeService dateTime,
            IConfiguration configuration,
            ILoggerFactory loggerFactory
        ) : base(options)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _configuration = configuration;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _loggerFactory = loggerFactory;
            _connectionString = this.Database.GetDbConnection().ConnectionString;
            #region Custom Constructor
            #endregion Custom Constructor


        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public virtual DbSet<AppClaim> AppClaims { get; set; }
        public virtual DbSet<AuditTrailLog> AuditTrailLogs { get; set; }
        public virtual DbSet<LogUserActivity> LogUserActivities { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            #region Custom Configuring
            #endregion Custom Configuring


            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new WebSiteLanguagesConfiguration());


            //  modelBuilder.Entity<BookType>().Property(p => p.RowVersion).IsRowVersion();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }


            #region Custom ModelCreating
            #endregion Custom ModelCreating



            base.OnModelCreating(modelBuilder);
        }




        private void OnBeforeSaveChanges(string userId)
        {

            #region Custom BeforeSaveChanges
            #endregion Custom BeforeSaveChanges



            var auditTrailLogEnabled = false;

            try
            {
                var auditTrailLog = bool.TryParse(_configuration["AuditTrailLogEnabled"], out auditTrailLogEnabled);
            }
            catch (Exception ex)
            {
                auditTrailLogEnabled = false;
            }

            if (auditTrailLogEnabled)
            {

                ChangeTracker.DetectChanges();

                var auditEntries = new List<AuditEntry>();
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.Entity is AuditTrailLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        continue;
                    var auditEntry = new AuditEntry();
                    auditEntry.TableName = entry.Entity.GetType().Name;
                    auditEntry.UserId = userId;

                    foreach (var property in entry.Properties)
                    {
                        string propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.AuditType = AppEnums.AuditType.Create;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                break;
                            case EntityState.Deleted:
                                auditEntry.AuditType = AppEnums.AuditType.Delete;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                break;
                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = AppEnums.AuditType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }

                    auditEntries.Add(auditEntry);
                }

                foreach (var auditEntry in auditEntries)
                {

                    AuditTrailLog auditTrailLog = new AuditTrailLog()
                    {

                        UserId = userId,
                        Type = auditEntry.AuditType.ToString(),
                        TableName = auditEntry.TableName,
                        ActionDateTime = DateTime.UtcNow,
                        PrimaryKey = JsonConvert.SerializeObject(auditEntry.KeyValues),
                        OldValues = auditEntry.OldValues.Count == 0
                            ? null
                            : JsonConvert.SerializeObject(auditEntry.OldValues),
                        NewValues = auditEntry.NewValues.Count == 0
                            ? null
                            : JsonConvert.SerializeObject(auditEntry.NewValues),
                        AffectedColumns = auditEntry.ChangedColumns.Count == 0
                            ? null
                            : JsonConvert.SerializeObject(auditEntry.ChangedColumns)
                    };

                    AuditTrailLogs.Add(auditTrailLog);
                }
            }
        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            // string userId = _httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string userId = _currentUserService.UserId;

            OnBeforeSaveChanges(userId);

            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = Guid.Parse(userId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = (string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId));
                        break;

                }
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.SoftDeleted = true;
                        entry.Entity.DeletedAt = _dateTime.NowUtc;
                        entry.Entity.DeletedBy = (string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId));
                        break;
                }
            }

            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var item in ex.Entries)
                {
                    if (item.Entity is IDataConcurrency)
                    {
                        var currentValues = item.CurrentValues;
                        var dbValues = item.GetDatabaseValues();

                        foreach (var prop in currentValues.Properties)
                        {
                            var currentValue = currentValues[prop];
                            var dbValue = dbValues[prop];
                        }

                        // Refresh the original values to bypass next concurrency check
                        item.OriginalValues.SetValues(dbValues);
                    }
                    else
                    {
                        throw new ApplicationException("Don’t know handling of concurrency conflict " + item.Metadata.Name);
                    }
                }
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                var sqlException = e.GetBaseException();
                if (sqlException != null)
                {
                    throw new ApplicationException(sqlException.Message, sqlException.InnerException); //return the error string
                }
                throw new ApplicationException("couldn’t handle that error"); //return the error string
                //couldn’t handle that error, so rethrow
            }


            #region Custom SaveChanges
            #endregion Custom SaveChanges


            return 0;
        }
        #region Custom
        #endregion Custom



        public async Task<IReadOnlyList<T>> QueryAsync<T>(string customConnectionString, string sql,
            bool isStoredProcedure, object param = null, IDbTransaction? transaction = null)
        {
            string cnString = (string.IsNullOrEmpty(customConnectionString) ? _connectionString : customConnectionString);

            using (SqlConnection sqlCon = new SqlConnection(cnString))
            {
                //  sqlCon.Open();
                if (isStoredProcedure)
                {
                    return sqlCon.QueryAsync<T>(sql, param, commandType: System.Data.CommandType.StoredProcedure).Result.ToList();
                }
                else
                {
                    return sqlCon.QueryAsync<T>(sql, param, commandType: System.Data.CommandType.Text).Result.ToList();
                }
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string customConnectionString, string sql,
            bool isStoredProcedure, object param = null, IDbTransaction? transaction = null)
        {
            string cnString =
                (string.IsNullOrEmpty(customConnectionString) ? _connectionString : customConnectionString);

            using (SqlConnection sqlCon = new SqlConnection(cnString))
            {
                //  sqlCon.Open();
                if (isStoredProcedure)
                {
                    return sqlCon.QueryAsync<T>(sql, param, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction).Result
                        .FirstOrDefault();
                }
                else
                {
                    return sqlCon.QueryAsync<T>(sql, param, commandType: System.Data.CommandType.Text, transaction: transaction).Result.FirstOrDefault();
                }
            }
        }
        public async Task<T> ExecuteReturnScalarAsync<T>(string customConnectionString, string sql, bool isStoredProcedure,
            object param = null, IDbTransaction? transaction = null)
        {
            string cnString =
                (string.IsNullOrEmpty(customConnectionString) ? _connectionString : customConnectionString);

            using (SqlConnection sqlCon = new SqlConnection(cnString))
            {
                // sqlCon.Open();
                if (isStoredProcedure)
                {
                    return (T)Convert.ChangeType(sqlCon.ExecuteScalarAsync<T>(sql, param, commandType: System.Data.CommandType.StoredProcedure,
                        transaction: transaction).Result, typeof(T));
                }
                else
                {
                    return (T)Convert.ChangeType(
                        sqlCon.ExecuteScalarAsync<T>(sql, param, commandType: System.Data.CommandType.Text, transaction: transaction).Result, typeof(T));
                }
            }
        }

        public void ExecuteWithoutReturnAsync(string customConnectionString, string sql, bool isStoredProcedure,
            object param = null, IDbTransaction? transaction = null)
        {
            string cnString =
                (string.IsNullOrEmpty(customConnectionString) ? _connectionString : customConnectionString);

            using (SqlConnection sqlCon = new SqlConnection(cnString))
            {
                //  sqlCon.Open();
                if (isStoredProcedure)
                {
                    sqlCon.ExecuteAsync(sql, param, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                }
                else
                {
                    sqlCon.ExecuteAsync(sql, param, commandType: System.Data.CommandType.Text, transaction: transaction);
                }
            }
        }

        public async Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, object[] parameters = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query must not be null or empty.", nameof(sql));

            return (List<T>)await this.Database.GetDbConnection().QueryAsync<T>(sql, parameters);
        }

        public async Task StartTransaction()
        {
            _dbContextTransaction = await this.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _dbContextTransaction.CommitAsync();
        }

        public async Task RollbackTransaction()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }


    }
}
