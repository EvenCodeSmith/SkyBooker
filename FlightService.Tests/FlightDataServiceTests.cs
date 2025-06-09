using FlightService.Models;
using FlightService.Services;
using MongoDB.Driver;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightService.Tests
{
    public class FlightDataServiceTests
    {
        private readonly Mock<IMongoCollection<Flight>> _mockCollection;
        private readonly FlightDataService _flightService;

        public FlightDataServiceTests()
        {
            _mockCollection = new Mock<IMongoCollection<Flight>>();
            _flightService = new FlightDataService(_mockCollection.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsAllFlights()
        {
            var testFlights = new List<Flight>
            {
                new Flight { Id = "1", FlightId = "FL123", AirlineName = "TestAir" }
            };

            var mockCursor = new Mock<IAsyncCursor<Flight>>();
            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                      .Returns(true)
                      .Returns(false);
            mockCursor.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true)
                      .ReturnsAsync(false);
            mockCursor.SetupGet(c => c.Current).Returns(testFlights);

            _mockCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Flight>>(),
                It.IsAny<FindOptions<Flight, Flight>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            var result = await _flightService.GetAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("FL123", result[0].FlightId);
        }

        [Fact]
        public async Task GetAsync_ById_ReturnsCorrectFlight()
        {
            var testFlight = new Flight
            {
                Id = "1",
                FlightId = "FL123",
                AirlineName = "TestAir"
            };

            var testList = new List<Flight> { testFlight };

            var mockCursor = new Mock<IAsyncCursor<Flight>>();
            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                      .Returns(true)
                      .Returns(false);
            mockCursor.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true)
                      .ReturnsAsync(false);
            mockCursor.SetupGet(c => c.Current).Returns(testList);

            _mockCollection.Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<Flight>>(),
                    It.IsAny<FindOptions<Flight, Flight>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            var result = await _flightService.GetAsync("1");

            Assert.NotNull(result);
            Assert.Equal("1", result!.Id);
            Assert.Equal("FL123", result.FlightId);
        }

        [Fact]
        public async Task CreateAsync_InsertsFlightCorrectly()
        {
            var testFlight = new Flight
            {
                Id = "1",
                FlightId = "FL123",
                AirlineName = "TestAir"
            };

            await _flightService.CreateAsync(testFlight);

            _mockCollection.Verify(c =>
                c.InsertOneAsync(
                    It.Is<Flight>(f => f.Id == "1" && f.FlightId == "FL123"),
                    null,
                    default),
                Times.Once);
        }
    }
}
