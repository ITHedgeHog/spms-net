using System;
using SPMS.Persistence.MSSQL;


namespace SPMS.Application.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly SpmsContext Context;

        public CommandTestBase()
        {
            Context = SpmsContextFactory.Create();
        }

        public void Dispose()
        {
            SpmsContextFactory.Destroy(Context);
        }
    }
}