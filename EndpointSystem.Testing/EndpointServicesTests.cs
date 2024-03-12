using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
using EndpointSystem.Application.Input.Model;
using Moq;
using AutoMapper;
using EndpointSystem.Application.Services.Implementation;
using EndpointsSystem.Domain.Enums;

namespace EndpointSystem.Application.Tests
{
    public class EndpointServiceTests
    {
        private readonly Mock<IEndpointRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EndpointService _service;

        public EndpointServiceTests()
        {
            _mockRepo = new Mock<IEndpointRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new EndpointService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateEndpoint_ExistingSerialNumber_ThrowsArgumentException()
        {
            // Arrange
            var input = new CreateEndpointInput { EndpointSerialNumber = "Test1", MeterModelId = EMeterModelId.NSX1P2W, MeterNumber = 100, MeterFirmwareVersion = "1.0", SwitchState = ESwitchState.Disconnected };
            _mockRepo.Setup(r => r.GetEndpointBySerialNumberAsync("Test1"))
                .ReturnsAsync(new Endpoint());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateEndpoint(input));

            _mockRepo.Verify(r => r.Create(It.IsAny<Endpoint>()), Times.Never);
        }

        [Fact]
        public async Task CreateEndpoint_UniqueSerialNumber_CreatesEndpointSuccessfully()
        {
            // Arrange
            var input = new CreateEndpointInput
            {
                EndpointSerialNumber = "uniqueSerialNumberTest",
                MeterModelId = EMeterModelId.NSX1P2W,
                MeterNumber = 200,
                MeterFirmwareVersion = "2.0",
                SwitchState = ESwitchState.Connected
            };

            _mockRepo.Setup(r => r.GetEndpointBySerialNumberAsync(input.EndpointSerialNumber))
                .ReturnsAsync((Endpoint)null);

            _mockMapper.Setup(m => m.Map<Endpoint>(It.IsAny<CreateEndpointInput>()))
                .Returns(new Endpoint
                {
                    EndpointSerialNumber = input.EndpointSerialNumber,
                    MeterModelId = input.MeterModelId,
                    MeterNumber = input.MeterNumber,
                    MeterFirmwareVersion = input.MeterFirmwareVersion,
                    SwitchState = input.SwitchState
                });

            // Act
            await _service.CreateEndpoint(input);

            // Assert
            _mockRepo.Verify(r => r.Create(It.Is<Endpoint>(e =>
                e.EndpointSerialNumber == input.EndpointSerialNumber &&
                e.MeterModelId == input.MeterModelId &&
                e.MeterNumber == input.MeterNumber &&
                e.MeterFirmwareVersion == input.MeterFirmwareVersion &&
                e.SwitchState == input.SwitchState)), Times.Once);

            _mockRepo.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}
