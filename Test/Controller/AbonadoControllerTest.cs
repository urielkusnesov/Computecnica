using Arquitectura.Controllers;
using Moq;
using NUnit.Framework;
using Service.Abonados;

namespace Test.Controller
{
    [TestFixture]
    public class AbonadoControllerTest
    {
        private AbonadoController target;
        private Mock<IAbonadoService> servcie;

        [SetUp]
        public void SetUp()
        {
            servcie = new Mock<IAbonadoService>();
        }

    }
}
