using NUnit.Framework;
using Moq;
using System.Web.Http;
using VehicleRegister.Domain.Interfaces;
using System.Collections.Generic;
using System;
using VehicleRegister.API.Controllers;
using VehicleRegister.Repository.Interfaces;
using VehicleRegister.DTO.dto;
using VehicleRegister.Providers;

namespace VehicleRegister.UnitTests
{
    [TestFixture]
    public class VehicleControllerTest
    {
        VehicleController controller;
        Mock<IVehicleFactory> VehicleFactoryMock;
        Mock<IService> ServiceMock;
        Mock<IVehicle> VehicleMock;
        List<IService> ServiceHistory;
        Mock<IVehicleRepository> RepositoryMock;
        Mock<IVehicleService> VehicleServiceMock;
        Mock<VehicleRequestDto> VehicleRequestDtoMock;
        Mock<OAuthWebApiAuthorizationServerProvider> ProviderMock;

        [SetUp]
        public void Setup()
        {
            VehicleFactoryMock = new Mock<IVehicleFactory>();
            ServiceMock = new Mock<IService>();
            VehicleMock = new Mock<IVehicle>();
            RepositoryMock = new Mock<IVehicleRepository>();
            VehicleServiceMock = new Mock<IVehicleService>();
            VehicleRequestDtoMock = new Mock<VehicleRequestDto>();
            ProviderMock = new Mock<OAuthWebApiAuthorizationServerProvider>();
            ServiceHistory = new List<IService>()
            {
                ServiceMock.Object
            };

            controller = new VehicleController(VehicleServiceMock.Object, RepositoryMock.Object);
        }

        [Test]
        public void ShouldRegisterVehicle()
        {
            //ProviderMock.Setup(p => p.GrantResourceOwnerCredentials())

            VehicleFactoryMock.Setup(v => v.CreateVehicle(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                                                          It.IsAny<double>(), It.IsAny<DateTime>(), It.IsAny<bool>(), 
                                                          ServiceMock.Object, ServiceHistory)).Returns(VehicleMock.Object);

            VehicleFactoryMock.Setup(v => v.CreateService(It.IsAny<DateTime>(), It.IsAny<string>())).Returns(ServiceMock.Object);

            var result = controller.RegisterVehicle(VehicleRequestDtoMock.Object);

            RepositoryMock.Verify(r => r.RegisterVehicle(VehicleMock.Object), Times.Once());
        }

        //Jag satte mig ner med lite av den extra tiden för att se om jag kunde lista ut hur mock fungerade på egen hand.
        //Jag kände att jag förstog det ganska väl, men jag fastnade med vad jag tror har att göra med att mina tokens blockar
        // testerna och jag kunde inte lista ut hur jag skulle mocka upp en token.
        // Jag lämnar denna ofärdiga kod här ifall det hjälper.
    }
}