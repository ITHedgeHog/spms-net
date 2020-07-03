using Microsoft.EntityFrameworkCore;
using System;

namespace SPMS.Persistence.PostgreSQL
{
    public class SpmsContextFactory : DesignTimeDbContextFactoryBase<SpmsContext>
    {
        protected override SpmsContext CreateNewInstance(DbContextOptions<SpmsContext> options)
        {
            return new SpmsContext(options);
        }
    }
}