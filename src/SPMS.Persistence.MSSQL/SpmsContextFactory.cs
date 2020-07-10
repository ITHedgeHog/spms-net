using Microsoft.EntityFrameworkCore;

namespace SPMS.Persistence.MSSQL
{
    public class SpmsContextFactory : DesignTimeDbContextFactoryBase<SpmsContext>
    {
        protected override SpmsContext CreateNewInstance(DbContextOptions<SpmsContext> options)
        {
            return new SpmsContext(options);
        }
    }
}