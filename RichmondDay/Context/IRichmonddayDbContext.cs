using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondDay.Context
{
    public interface IRichmonddayDbContext
    {
        DbSet<RichmonddayInfo> RichmonddayInfoes { get; set; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
