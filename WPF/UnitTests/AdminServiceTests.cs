using FluentAssertions;
using Moq;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;


namespace UnitTests
{
    public class AdminServiceTests
    {
        [Fact]
        public void GetAllClasses_ShouldReturnOrderedClasses()
        {
            // Arrange
            var mockClassRepository = new Mock<GenericRepository<Class>>();
            var adminService = new AdminService(null, mockClassRepository.Object, null, null, null, null, null, null, null, null);

            var classes = new List<Class>
            {
                new Class { Id = 1, ClassName = "11-А" },
                new Class { Id = 2, ClassName = "5-Б" },
            };

            mockClassRepository.Setup(repo => repo.GetAll()).Returns(classes.AsEnumerable());

            // Act
            var result = adminService.GetAllClasses();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().ClassName.Should().Be("11-А");
            result.Last().ClassName.Should().Be("5-Б");
            result.Should().BeEquivalentTo(classes.OrderBy(x => x.ClassName));
        }

        [Fact]
        public void GetAllAdmins_ShouldReturnOrderedDescendingAdmins()
        {
            // Arrange
            var mockUserRepository = new Mock<GenericRepository<User>>();
            var adminService = new AdminService(null, null, null, null, null, null, null, null, null, mockUserRepository.Object);

            var admins = new List<User>
            {
                new User { Id = 1, Role = "адмін", LastName = "Арат" },
                new User { Id = 2, Role = "адмін", LastName = "Петренко" },
            };

            mockUserRepository.Setup(repo => repo.GetAll()).Returns(admins.AsEnumerable());

            // Act
            var result = adminService.GetAllAdmins();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().LastName.Should().Be("Петренко");
            result.Last().LastName.Should().Be("Арат");
            result.Should().BeEquivalentTo(admins.OrderBy(x => x.LastName));
        }

        [Fact]
        public void GetAllTeachers_ShouldReturnOrderedDescendingTeachers()
        {
            // Arrange
            var mockUserRepository = new Mock<GenericRepository<User>>();
            var adminService = new AdminService(null, null, null, null, null, null, null, null, null, mockUserRepository.Object);

            var teachers = new List<User>
            {
                new User { Id = 1, Role = "вчитель", LastName = "Богданенко" },
                new User { Id = 2, Role = "вчитель", LastName = "Мартенюк" },
            };

            mockUserRepository.Setup(repo => repo.GetAll()).Returns(teachers.AsEnumerable());

            // Act
            var result = adminService.GetAllTeacher();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().LastName.Should().Be("Мартенюк");
            result.Last().LastName.Should().Be("Богданенко");
            result.Should().BeEquivalentTo(teachers.OrderBy(x => x.LastName));
        }

        [Fact]
        public void GetAllParents_ShouldReturnOrderedDescendingParents()
        {
            // Arrange
            var mockUserRepository = new Mock<GenericRepository<User>>();
            var adminService = new AdminService(null, null, null, null, null, null, null, null, null, mockUserRepository.Object);

            var parents = new List<User>
            {
                new User { Id = 1, Role = "батьки", LastName = "Іващенко" },
                new User { Id = 2, Role = "батьки", LastName = "Коновалець" },
            };

            mockUserRepository.Setup(repo => repo.GetAll()).Returns(parents.AsEnumerable());

            // Act
            var result = adminService.GetAllParents();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().LastName.Should().Be("Коновалець");
            result.Last().LastName.Should().Be("Іващенко");
            result.Should().BeEquivalentTo(parents.OrderBy(x => x.LastName));
        }

        [Fact]
        public void GetAllPupils_ShouldReturnOrderedDescendingPupils()
        {
            // Arrange
            var mockUserRepository = new Mock<GenericRepository<User>>();
            var adminService = new AdminService(null, null, null, null, null, null, null, null, null, mockUserRepository.Object);

            var pupils = new List<User>
            {
                new User { Id = 1, Role = "учень", LastName = "Савчук" },
                new User { Id = 2, Role = "учень", LastName = "Іванов" },
                new User { Id = 3, Role = "вчитель", LastName = "Абрамов" },
            };

            mockUserRepository.Setup(repo => repo.GetAllq()).Returns(pupils.AsQueryable());

            // Act
            var result = adminService.GetAllPupils();

            // Assert
            result.Should().HaveCount(2);
            result.Should().BeInDescendingOrder(u => u.LastName);
            result.Should().Contain(u => u.Id == 1 && u.LastName == "Савчук");
            result.Should().Contain(u => u.Id == 2 && u.LastName == "Іванов");
        }

        [Fact]
        public void GetAllSubjectsForTeacher_ShouldReturnCorrectSubjects()
        {
            // Arrange
            var mockSubjectRepository = new Mock<GenericRepository<Subject>>();
            var mockTeacherRepository = new Mock<GenericRepository<Teacher>>();
            var mockUserRepository = new Mock<GenericRepository<User>>();
            var adminService = new AdminService(null, null, null, null, null, null, null, mockSubjectRepository.Object, mockTeacherRepository.Object, mockUserRepository.Object);

            var teacherId = 1;

            var subjects = new List<Subject>
            {
                 new Subject { Id = 1, SubjectName = "Алгебра", Teachers = new List<Teacher> { new Teacher { UserId = teacherId } } },
                 new Subject { Id = 2, SubjectName = "Хімія", Teachers = new List<Teacher> { new Teacher { UserId = teacherId + 1 } } },
            };

            mockSubjectRepository.Setup(r => r.GetAllq()).Returns(subjects.AsQueryable());

            // Act
            var result = adminService.GetAllSubjectsForTeacher(teacherId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result[0].SubjectName.Should().Be("Алгебра");
        }

        [Fact]
        public void GetNewDayBookId_ShouldReturnNextId_WhenExistingDayBooks()
        {
            // Arrange
            var dayBookRepositoryMock = new Mock<GenericRepository<DayBook>>();
            var adminService = new AdminService(null, null, dayBookRepositoryMock.Object, null, null, null, null, null, null, null);
            
            var dayBookList = new List<DayBook>
            {
                new DayBook { Id = 1 },
                new DayBook { Id = 2 },
                new DayBook { Id = 3 }
            };

            dayBookRepositoryMock.Setup(repo => repo.GetAll()).Returns(dayBookList);

            // Act
            var result = adminService.GetNewDayBookId();

            // Assert
            result.Should().Be(4);
        }
    }
}
