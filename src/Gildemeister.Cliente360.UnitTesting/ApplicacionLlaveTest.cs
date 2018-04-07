using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Transport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.UnitTesting
{
    [TestClass]
    public class ApplicacionLlaveTest
    {
        private IApplicacionLlaveService applicacionLlaveService;

        public ApplicacionLlaveTest(IApplicacionLlaveService applicacionLlaveService)
        {
            this.applicacionLlaveService = applicacionLlaveService;
        }

        [TestMethod]
        public void TestMethod1()
        {
            ApplicacionLlaveDTO applicacionLlave =null;

            Assert.IsNull(applicacionLlave.FechaCambio);
        }
    }
}
