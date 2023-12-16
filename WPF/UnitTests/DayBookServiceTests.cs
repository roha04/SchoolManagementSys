using FluentAssertions;
using Moq;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class DayBookServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllDayBooksFromRepository()
        {
            // Arrange
            var dayBookRepositoryMock = new Mock<GenericRepository<DayBook>>();
            var dayBookService = new DayBookService(dayBookRepositoryMock.Object);

            var DayBooks = new List<DayBook>
            {
                new DayBook { Id = 1 },
                new DayBook { Id = 2 }
            };

            dayBookRepositoryMock.Setup(repo => repo.GetAll()).Returns(DayBooks.AsEnumerable());

            // Act
            var result = dayBookService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(DayBooks);
        }
    }
}
