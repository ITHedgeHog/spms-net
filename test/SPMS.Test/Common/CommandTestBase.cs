using System;
using SPMS.Persistence.PostgreSQL;
using SpmsContextFactory = SPMS.Test.Common.SpmsContextFactory;

namespace SPMS.Test.Common
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