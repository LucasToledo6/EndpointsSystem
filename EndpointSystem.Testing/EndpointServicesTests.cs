using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
using EndpointSystem.Application.Input.Model;
using Moq;
using AutoMapper;
using EndpointSystem.Application.Services.Implementation;
using EndpointsSystem.Domain.Enums;
using EndpointSystem.Application.DTO;

namespace EndpointSystem.Application.Tests
{
    public class EndpointServiceTests
    {
        private readonly Mock<IEndpointRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EndpointService _endpointService;

        public EndpointServiceTests()
        {
            _mockRepository = new Mock<IEndpointRepository>();
            _mockMapper = new Mock<IMapper>();
            _endpointService = new EndpointService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateEndpoint_WhenExistingSerialNumber_ShouldThrowArgumentException()
        {
            // Arrange
            var createEndpointInput = new CreateEndpointInput 
            { 
                EndpointSerialNumber = "UnitTesting1", 
                MeterModelId = EMeterModelId.NSX1P2W, 
                MeterNumber = 777, 
                MeterFirmwareVersion = "77.0", 
                SwitchState = ESwitchState.Disconnected 
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync("UnitTesting1")).ReturnsAsync(new Endpoint());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _endpointService.CreateEndpoint(createEndpointInput));

            _mockRepository.Verify(x => x.Create(It.IsAny<Endpoint>()), Times.Never);
        }

        [Fact]
        public async Task CreateEndpoint_WhenUniqueSerialNumber_ShouldCreateEndpointSuccessfully()
        {
            // Arrange
            var createEndpointInput = new CreateEndpointInput
            {
                EndpointSerialNumber = "UnitTesting2",
                MeterModelId = EMeterModelId.NSX3P4W,
                MeterNumber = 42,
                MeterFirmwareVersion = "42.0",
                SwitchState = ESwitchState.Connected
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(createEndpointInput.EndpointSerialNumber)).ReturnsAsync((Endpoint)null);

            _mockMapper.Setup(x => x.Map<Endpoint>(It.IsAny<CreateEndpointInput>())).Returns(new Endpoint
                {
                    EndpointSerialNumber = createEndpointInput.EndpointSerialNumber,
                    MeterModelId = createEndpointInput.MeterModelId,
                    MeterNumber = createEndpointInput.MeterNumber,
                    MeterFirmwareVersion = createEndpointInput.MeterFirmwareVersion,
                    SwitchState = createEndpointInput.SwitchState
                });

            // Act
            await _endpointService.CreateEndpoint(createEndpointInput);

            // Assert
            _mockRepository.Verify(x => x.Create(It.Is<Endpoint>(endpoint =>
                endpoint.EndpointSerialNumber == createEndpointInput.EndpointSerialNumber &&
                endpoint.MeterModelId == createEndpointInput.MeterModelId &&
                endpoint.MeterNumber == createEndpointInput.MeterNumber &&
                endpoint.MeterFirmwareVersion == createEndpointInput.MeterFirmwareVersion &&
                endpoint.SwitchState == createEndpointInput.SwitchState)), Times.Once);

            _mockRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task EditEndpoint_WhenEndpointNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            var editEndpointInput = new EditEndpointInput
            {
                EndpointSerialNumber = "UnitTesting3",
                SwitchState = ESwitchState.Disconnected
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(editEndpointInput.EndpointSerialNumber)).ReturnsAsync((Endpoint)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _endpointService.EditEndpoint(editEndpointInput));
        }

        [Fact]
        public async Task EditEndpoint_WhenNewSwitchStateIsSameAsCurrent_ShouldThrowArgumentException()
        {
            // Arrange
            var existingEndpoint = new Endpoint { SwitchState = ESwitchState.Connected };
            var editEndpointInput = new EditEndpointInput
            {
                EndpointSerialNumber = "UnitTesting4",
                SwitchState = ESwitchState.Connected
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(editEndpointInput.EndpointSerialNumber)).ReturnsAsync(existingEndpoint);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _endpointService.EditEndpoint(editEndpointInput));
        }

        [Fact]
        public async Task EditEndpoint_WhenEndpointExistsAndNewSwitchStateIsDifferent_ShouldUpdateEndpoint()
        {
            // Arrange
            var existingEndpoint = new Endpoint { SwitchState = ESwitchState.Connected };
            var editEndpointInput = new EditEndpointInput
            {
                EndpointSerialNumber = "UnitTesting5",
                SwitchState = ESwitchState.Disconnected
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(editEndpointInput.EndpointSerialNumber)).ReturnsAsync(existingEndpoint);

            // Act
            await _endpointService.EditEndpoint(editEndpointInput);

            // Assert
            _mockRepository.Verify(x => x.Update(It.Is<Endpoint>(endpoint => endpoint.SwitchState == editEndpointInput.SwitchState)), Times.Once);
            _mockRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEndpoint_WhenEndpointNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            string endpointSerialNumber = "UnitTesting6";
            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(endpointSerialNumber)).ReturnsAsync((Endpoint)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _endpointService.DeleteEndpoint(endpointSerialNumber));
        }

        [Fact]
        public async Task DeleteEndpoint_WhenEndpointExists_ShouldDeleteEndpoint()
        {
            // Arrange
            var existingEndpoint = new Endpoint();
            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(existingEndpoint);

            // Act
            await _endpointService.DeleteEndpoint("UnitTesting7");

            // Assert
            _mockRepository.Verify(x => x.Delete(It.Is<Endpoint>(endpoint => endpoint == existingEndpoint)), Times.Once);
            _mockRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task FindEndpoint_WhenEndpointNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            string endpointSerialNumber = "UnitTesting8";
            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(endpointSerialNumber))
                .ReturnsAsync((Endpoint)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _endpointService.FindEndpoint(endpointSerialNumber));
        }

        [Fact]
        public async Task FindEndpoint_WhenEndpointExists_ShouldReturnEndpointDto()
        {
            // Arrange
            string endpointSerialNumber = "UnitTesting9";
            var endpoint = new Endpoint
            {
                EndpointSerialNumber = endpointSerialNumber,
                MeterModelId = EMeterModelId.NSX1P2W,
                MeterNumber = 11111,
                MeterFirmwareVersion = "9.9.1",
                SwitchState = ESwitchState.Armed
            };
            var endpointDto = new EndpointDto
            {
                EndpointSerialNumber = endpointSerialNumber,
                MeterModelId = EMeterModelId.NSX1P2W,
                MeterNumber = 11111,
                MeterFirmwareVersion = "9.9.1",
                SwitchState = ESwitchState.Armed
            };

            _mockRepository.Setup(x => x.GetEndpointBySerialNumberAsync(endpointSerialNumber)).ReturnsAsync(endpoint);
            _mockMapper.Setup(x => x.Map<EndpointDto>(It.IsAny<Endpoint>())).Returns(endpointDto);

            // Act
            var foundEndpoint = await _endpointService.FindEndpoint(endpointSerialNumber);

            // Assert
            Assert.NotNull(foundEndpoint);
            Assert.Equal(endpointSerialNumber, foundEndpoint.EndpointSerialNumber);
            Assert.Equal(EMeterModelId.NSX1P2W, foundEndpoint.MeterModelId);
            Assert.Equal(11111, foundEndpoint.MeterNumber);
            Assert.Equal("9.9.1", foundEndpoint.MeterFirmwareVersion);
            Assert.Equal(ESwitchState.Armed, foundEndpoint.SwitchState);
            _mockMapper.Verify(x => x.Map<EndpointDto>(It.IsAny<Endpoint>()), Times.Once);
        }

        [Fact]
        public async Task ListAllEndpoints_WhenNoEndpointsExist_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAllEndpoints()).ReturnsAsync(new List<Endpoint>());
            _mockMapper.Setup(x => x.Map<List<EndpointDto>>(It.IsAny<List<Endpoint>>())).Returns(new List<EndpointDto>());

            // Act
            var endpointList = await _endpointService.ListAllEndpoints();

            // Assert
            Assert.Empty(endpointList);
        }

        [Fact]
        public async Task ListAllEndpoints_WhenEndpointsExist_ShouldReturnListOfEndpointDtos()
        {
            // Arrange
            var endpoints = new List<Endpoint>
            {
                new Endpoint { EndpointSerialNumber = "PhiladelphiaEagles52", MeterModelId = EMeterModelId.NSX3P4W, MeterNumber = 41, MeterFirmwareVersion = "41.0", SwitchState = ESwitchState.Connected },
                new Endpoint { EndpointSerialNumber = "NewEnglandPatriots52", MeterModelId = EMeterModelId.NSX2P3W, MeterNumber = 33, MeterFirmwareVersion = "33.0", SwitchState = ESwitchState.Disconnected }
            };

            var endpointDtos = new List<EndpointDto>
            {
                new EndpointDto { EndpointSerialNumber = "PhiladelphiaEagles52", MeterModelId = EMeterModelId.NSX3P4W, MeterNumber = 41, MeterFirmwareVersion = "41.0", SwitchState = ESwitchState.Connected },
                new EndpointDto { EndpointSerialNumber = "NewEnglandPatriots52", MeterModelId = EMeterModelId.NSX2P3W, MeterNumber = 33, MeterFirmwareVersion = "33.0", SwitchState = ESwitchState.Disconnected }
            };

            _mockRepository.Setup(x => x.GetAllEndpoints()).ReturnsAsync(endpoints);
            _mockMapper.Setup(x => x.Map<List<EndpointDto>>(It.IsAny<List<Endpoint>>())).Returns(endpointDtos);

            // Act
            var endpointList = await _endpointService.ListAllEndpoints();

            // Assert
            Assert.NotEmpty(endpointList);
            Assert.Equal(2, endpointList.Count);
            Assert.Equal("PhiladelphiaEagles52", endpointList[0].EndpointSerialNumber);
            Assert.Equal("NewEnglandPatriots52", endpointList[1].EndpointSerialNumber);
            _mockMapper.Verify(x => x.Map<List<EndpointDto>>(endpoints), Times.Once);
        }
    }
}
