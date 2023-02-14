using Microsoft.Extensions.Configuration;
using MilanCiCdExample;
using MilanCiCdExample.Controllers;

namespace MilanCiCdTests
{
    public class DemoTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }
    }
}