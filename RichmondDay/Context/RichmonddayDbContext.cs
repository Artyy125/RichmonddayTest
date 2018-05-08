namespace RichmondDay.Context
{
    using System.Data.Entity;

    public class RichmonddayDbContext : DbContext,IRichmonddayDbContext
    {
        public RichmonddayDbContext()
            : base("name=RichmonddayDbContext")
        {
        }

        public virtual DbSet<RichmonddayInfo> RichmonddayInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
