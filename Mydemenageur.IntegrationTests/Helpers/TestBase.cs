using Microsoft.AspNetCore.Mvc.Testing;
using Mydemenageur.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mydemenageur.IntegrationTests.Helpers
{
    public abstract class TestBase : IClassFixture<TestApplicationFactory<Startup, FakeStartup>>
    {
        protected WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase(TestApplicationFactory<Startup, FakeStartup> factory)
        {
            Factory = factory;
        }
    }
}
