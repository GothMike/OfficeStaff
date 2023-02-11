using Microsoft.EntityFrameworkCore;

namespace OfficeStaff.Persistence
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions optins) : base(optins)
        {
        }
    }
}
