using Gildemeister.Cliente360.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gildemeister.Cliente360.UnitTesting
{
    [TestClass]
    public class ClienteUnitTest
    {
        private IClienteService clienteService;

        public ClienteUnitTest(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
