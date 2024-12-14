using Moq;
using FluentAssertions;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class TeacherServiceTests
    {
        [Fact]
        public void AddTeacher_ShouldInsertUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var advertisementRepositoryMock = new Mock<GenericRepository<Advertisement>>();
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var scheduleRepositoryMock = new Mock<GenericRepository<Schedule>>();

            var teacherService = new TeacherService(
                advertisementRepositoryMock.Object,
                userRepositoryMock.Object,
                teacherRepositoryMock.Object,
                subjectRepositoryMock.Object,
                scheduleRepositoryMock.Object
            );

            var user = new User { Id = 1, FirstName = "Олег", LastName = "Петренко", MiddleName = "Іванович", Gender = "чоловік" };

            userRepositoryMock.Setup(repo => repo.Insert(user));
            userRepositoryMock.Setup(repo => repo.Save());

            // Act
            var result = teacherService.AddTeacher(user);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("Олег");
        }

        [Fact]
        public void GetAllAdvertisementsForClassId_ShouldReturnAdvertisements()
        {
            // Arrange
            var advertisementRepositoryMock = new Mock<GenericRepository<Advertisement>>();
            var userReposMock = new Mock<GenericRepository<User>>();
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var scheduleRepositoryMock = new Mock<GenericRepository<Schedule>>();

            var teacherService = new TeacherService(
                advertisementRepositoryMock.Object,
                userReposMock.Object,
                teacherRepositoryMock.Object,
                subjectRepositoryMock.Object,
                scheduleRepositoryMock.Object
            );

            var classId = 1;
            var advertisements = new List<Advertisement>
            {
                new Advertisement { Id = 1, Name = "Важливо", ClassId = classId },
                new Advertisement { Id = 2, Name = "На завтра", ClassId = classId + 1 },
            };

            advertisementRepositoryMock.Setup(repo => repo.GetAllq()).Returns(advertisements.AsQueryable());

            // Act
            var result = teacherService.GetAllAdvertisementsForClassId(classId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Should().Contain(ad => ad.Name == "Важливо" && ad.ClassId == classId);
        }

        [Fact]
        public void ShowAllSubjectsForTeacherId_ShouldReturnCorrectSubjects()
        {
            // Arrange
            var teacherId = 1;
            var subjectRepositoryMock = new Mock<GenericRepository<Subject>>();
            var teacherService = new TeacherService(null, null, null, subjectRepositoryMock.Object, null);

            var subjects = new List<Subject>
            {
                new Subject { Id = 1, SubjectName = "Алгебра", Teachers = new List<Teacher> { new Teacher { UserId = 1 } } },
                new Subject { Id = 2, SubjectName = "Фізика", Teachers = new List<Teacher> { new Teacher { UserId = 1 } } }
            };

            subjectRepositoryMock.Setup(repo => repo.GetAllq()).Returns(subjects.AsQueryable());

            // Act
            var result = teacherService.ShowAllSubjectsForTeacherId(teacherId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); 
            result.Should().Contain(subject => subject.SubjectName == "Алгебра");
            result.Should().Contain(subject => subject.SubjectName == "Фізика");
        }

        [Fact]
        public void GetAllTeacherIds_ShouldReturnCorrectTeacherIds()
        {
            // Arrange
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();
            var teacherService = new TeacherService(null, null, teacherRepositoryMock.Object, null, null);

            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1 },
                new Teacher { Id = 2 },
            };

            teacherRepositoryMock.Setup(repo => repo.GetAll()).Returns(teachers.AsEnumerable());

            // Act
            var result = teacherService.GetAllTeacherIds();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(teacherId => teacherId == 1);
            result.Should().Contain(teacherId => teacherId == 2);
        }

        [Fact]
        public void GetTeacherBySubjectId_ShouldReturnTeacher_WhenSubjectExists()
        {
            // Arrange
            var subjectId = 1;
            var teacherRepositoryMock = new Mock<GenericRepository<Teacher>>();
            var teacherService = new TeacherService(null, null, teacherRepositoryMock.Object, null, null);

            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, SubjectId = subjectId },
                new Teacher { Id = 2, SubjectId = subjectId },
                new Teacher { Id = 3, SubjectId = subjectId + 1 }
            };

            teacherRepositoryMock.Setup(repo => repo.GetAll()).Returns(teachers.AsEnumerable());

            // Act
            var result = teacherService.GetTeacherBySubjectId(subjectId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}
