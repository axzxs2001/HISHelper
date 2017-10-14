using Microsoft.EntityFrameworkCore;


namespace Working.Models.DataModel
{

    public class WorkingDbContext : DbContext
    {
        public WorkingDbContext(DbContextOptions<WorkingDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

     

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(m => m.ID);
            builder.Entity<WorkItem>().HasKey(m => m.ID);          
            base.OnModelCreating(builder);
        }
    }
}
