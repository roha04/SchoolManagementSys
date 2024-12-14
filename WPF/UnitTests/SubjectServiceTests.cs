using Moq;
using FluentAssertions;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class SubjectServiceTests
    {
        [Fact]
        public void GetSubjectById_ShouldReturnCorrectSubject()
        {
            // Arrange
            var subjectId = 1;
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();
            var subjectService = new SubjectService(subjectRepositoryMock.Object, teacherRepositoryMock.Object);

            var subjects = new List<Subject>
            {
                new Subject { Id = 1, SubjectName = "Алгебра", ClassId = 1 },
                new Subject { Id = 2, SubjectName = "Фізика", ClassId = 2 },
            };

            subjectRepositoryMock.Setup(repo => repo.GetAll()).Returns(subjects.AsEnumerable());

            // Act
            var result = subjectService.GetSubjectById(subjectId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.SubjectName.Should().Be("Алгебра");
        }

        [Fact]
        public void GetSubjectsByClassId_ShouldReturnCorrectSubjects()
        {
            // Arrange
            var classId = 1;
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();

            var subjectService = new SubjectService(subjectRepositoryMock.Object, teacherRepositoryMock.Object);

            var subjects = new List<Subject>
            {
                new Subject { Id = 1, SubjectName = "Алгебра", ClassId = 1 },
                new Subject { Id = 2, SubjectName = "Фізика", ClassId = 2 },
                new Subject { Id = 3, SubjectName = "Історія", ClassId = 1 },
            };

            subjectRepositoryMock.Setup(repo => repo.GetAll()).Returns(subjects.AsEnumerable());

            // Act
            var result = subjectService.GetSubjectsByClassId(classId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(s => s.Id == 1 && s.SubjectName == "Алгебра");
            result.Should().Contain(s => s.Id == 3 && s.SubjectName == "Історія");
        }

        [Fact]
        public void GetAllSubjects_ShouldReturnSubjectsOrderedByName()
        {
            // Arrange
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();

            var subjectService = new SubjectService(subjectRepositoryMock.Object, teacherRepositoryMock.Object);

            var subjects = new List<Subject>
            { 
                new Subject { Id = 1, SubjectName = "Алгебра", ClassId = 1 },
                new Subject { Id = 2, SubjectName = "Фізика", ClassId = 2 }
            };

            subjectRepositoryMock.Setup(repo => repo.GetAll()).Returns(subjects.AsEnumerable());

            // Act
            var result = subjectService.GetAllSubjects();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeInAscendingOrder(s => s.SubjectName);
        }
    }
}
