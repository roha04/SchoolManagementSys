using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public void Authenticate_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object, null, null, null);
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<User> { new User { Email = "test@gmail.com", Password = "password" } }.AsEnumerable());

            // Act
            var result = userService.Authenticate("test@gmail.com", "password");

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@gmail.com");
            result.Password.Should().Be("password");
        }

        [Fact]
        public void AuthenticateEmail_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object, null, null, null);
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<User> { new User { Email = "test@gmail.com" } }.AsEnumerable());

            // Act
            var result = userService.AuthenticateEmail("test@gmail.com");

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@gmail.com");
        }

        [Fact]
        public void AuthenticatePassword_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object, null, null, null);
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<User> { new User { Password = "password" } }.AsEnumerable());

            // Act
            var result = userService.AuthenticatePassword("password");

            // Assert
            result.Should().NotBeNull();
            result.Password.Should().Be("password");
        }

        [Fact]
        public void GetInfoByNameSurname_UserExists_ReturnsUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object, null, null, null);
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<User> { new User { FirstName = "Олег", LastName = "Петренко", MiddleName = "Іванович", Gender = "чоловік" } }.AsEnumerable());

            // Act
            var result = userService.GetInfoByNameSurname("Олег", "Петренко");

            // Assert
            result.Should().NotBeNull();
            result.Gender.Should().Be("чоловік");
        }

        [Fact]
        public void AddUser_ShouldInsertUserAndPupil()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var parentRepositoryMock = new Mock<GenericRepository<Parents>>();
            var parentsPupilRepositoryMock = new Mock<GenericRepository<ParentsPupil>>();

            var userService = new UserService(
                userRepositoryMock.Object,
                pupilRepositoryMock.Object,
                parentRepositoryMock.Object,
                parentsPupilRepositoryMock.Object
            );

            var user = new User { Id = 1, FirstName = "Олег", LastName = "Петренко", MiddleName = "Іванович", Gender = "чоловік" };
            var pupil = new Pupil { Id = 1, UserId = 1 };

            userRepositoryMock.Setup(repo => repo.Insert(user));
            userRepositoryMock.Setup(repo => repo.Save());
            pupilRepositoryMock.Setup(repo => repo.Insert(pupil));
            pupilRepositoryMock.Setup(repo => repo.Save());


            // Act
            var result = userService.AddUser(user, pupil);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("Олег");
        }

        [Fact]
        public void AddUser_ShouldInsertUserAndParentAndParentsPupil()
        {
            // Arrange
            var pupilId = 1;
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var parentRepositoryMock = new Mock<GenericRepository<Parents>>();
            var parentsPupilRepositoryMock = new Mock<GenericRepository<ParentsPupil>>();

            var userService = new UserService(
                userRepositoryMock.Object, 
                pupilRepositoryMock.Object, 
                parentRepositoryMock.Object, 
                parentsPupilRepositoryMock.Object);

            var user = new User { Id = 1, FirstName = "Олег", LastName = "Петренко", MiddleName = "Іванович", Gender = "чоловік" };
            var parents = new Parents { Id = 1, UserId = 1 };
            var parentsPupil = new ParentsPupil{ PupilId = pupilId, ParentId = parents.Id };

            userRepositoryMock.Setup(repo => repo.Insert(user));
            userRepositoryMock.Setup(repo => repo.Save());
            parentRepositoryMock.Setup(repo => repo.Insert(parents));
            parentRepositoryMock.Setup(repo => repo.Save());
            parentsPupilRepositoryMock.Setup(repo => repo.Insert(parentsPupil));
            parentsPupilRepositoryMock.Setup(repo => repo.Save());


            // Act
            var result = userService.AddUser(user, parents, pupilId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("Олег");
        }

        [Fact]
        public void ShowUsersForSubjectId_ShouldReturnUsersForSubjectId()
        {
            // Arrange
            int subjectId = 1;
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var parentRepositoryMock = new Mock<GenericRepository<Parents>>();
            var parentsPupilRepositoryMock = new Mock<GenericRepository<ParentsPupil>>();

            var userService = new UserService(
                userRepositoryMock.Object,
                pupilRepositoryMock.Object,
                parentRepositoryMock.Object,
                parentsPupilRepositoryMock.Object
            );

            var users = new List<User>
            {
                new User { Id = 1, Pupil = new Pupil { Class = new Class { Subjects = new List<Subject> { new Subject { Id = subjectId } } } } },
                new User { Id = 2, Pupil = new Pupil { Class = new Class { Subjects = new List<Subject> { new Subject { Id = subjectId + 1 } } } } }
            };

            userRepositoryMock.Setup(repo => repo.GetAllq()).Returns(users.AsQueryable().Include(x => x.Pupil!.Class!.Subjects));

            // Act
            var result = userService.ShowUsersForSubjectId(subjectId);

            // Assert
            result.Should().HaveCount(1);
            result[0].Id.Should().Be(1);
        }

        [Fact]
        public void GetUserById_ShouldReturnUserById()
        {
            // Arrange
            var userRepositoryMock = new Mock<GenericRepository<User>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var parentRepositoryMock = new Mock<GenericRepository<Parents>>();
            var parentsPupilRepositoryMock = new Mock<GenericRepository<ParentsPupil>>();

            var userService = new UserService(
                userRepositoryMock.Object,
                pupilRepositoryMock.Object,
                parentRepositoryMock.Object,
                parentsPupilRepositoryMock.Object
            );

            var userId = 1;
            var users = new List<User>
            {
                new User { Id = userId, FirstName = "Олег", LastName = "Петренко", MiddleName = "Іванович", Gender = "чоловік" },
                new User { Id = userId + 1, FirstName = "Марія", LastName = "Шевченко", MiddleName = "Степанівна", Gender = "жінка" }
            };

            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users.AsEnumerable());

            // Act
            var result = userService.GetUserById(userId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.FirstName.Should().Be("Олег");
        }
    }
}
