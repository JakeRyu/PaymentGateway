using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common.DateService;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }
        
        public DbSet<Payment> Payments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}