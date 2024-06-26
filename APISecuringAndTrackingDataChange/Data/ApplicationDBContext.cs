



using System.Text;

namespace MyAPISecuringAndTrackingDataChange.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added
                         || x.State == EntityState.Deleted
                         || x.State == EntityState.Modified)
                .ToList();

            foreach (var me in modifiedEntities)
            {
                var auditLog = new AuditLog
                {
                    Action = me.State.ToString(),
                    TimeStamp = DateTime.UtcNow,
                    EntityType = me.Entity.GetType().Name,
                    Changes = GetUpdate(me)
                };

                AuditLogs.Add(auditLog);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private static string GetUpdate(EntityEntry entityEntry)
        {
            var sb = new StringBuilder();

            foreach (var prop in entityEntry.OriginalValues.Properties)
            {
                var originalValue = entityEntry.OriginalValues[prop];
                var currentValue = entityEntry.CurrentValues[prop];

                if (!Equals(originalValue, currentValue))
                {
                    sb.Append($"{prop.Name} : from {originalValue} to {currentValue} ");
                }
            }

            return sb.ToString();


        }

    }
}
