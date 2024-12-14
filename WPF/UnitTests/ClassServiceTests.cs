using FluentAssertions;
using Moq;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class ClassServiceTests
    {
        [Fact]
        public void GetAllClasses_ShouldReturnAllClasses()
        {
            // Arrange
            var mockRepository = new Mock<GenericRepository<Class>>();
            var classService = new ClassService(mockRepository.Object);

            var classes = new List<Class>
            {
                new Class { Id = 1, ClassName = "11-А" },
                new Class { Id = 2, ClassName = "5-Б" },
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(classes.AsEnumerable());

            // Act
            var result = classService.GetAllClasses();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(classes);
        }

        [Fact]
        public void GetClassBySubjectId_ShouldReturnClassForGivenSubjectId()
        {
            // Arrange
            var mockRepository = new Mock<GenericRepository<Class>>();
            var classService = new ClassService(mockRepository.Object);

            var subjectId = 1;
            var classes = new List<Class>
            {
            new Class { Id = 1, Subjects = new List<Subject> { new Subject { Id = subjectId } } },
            new Class { Id = 2, Subjects = new List<Subject> { new Subject { Id = 2 } } }
            };

            mockRepository.Setup(repo => repo.GetAllq()).Returns(classes.AsQueryable());

            // Act
            var result = classService.GetClassBySubjectId(subjectId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(classes.First());
        }
    }
}
