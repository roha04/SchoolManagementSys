using FluentAssertions;
using Moq;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace UnitTests
{
    public class AdvertisementServiceTests
    {
        [Fact]
        public void GetAllAdvertisementsForClassId_ShouldReturnAdvertisementsForClass()
        {
            // Arrange
            var advertisementRepositoryMock = new Mock<GenericRepository<Advertisement>>();
            var classRepositoryMock = new Mock<GenericRepository<Class>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var advertisementService = new AdvertisementService(advertisementRepositoryMock.Object, classRepositoryMock.Object, pupilRepositoryMock.Object);

            var classId = 1;
            var advertisementsForClass = new List<Advertisement> 
            { 
                new Advertisement { Id = 1, Name = "Важливо", ClassId = classId },
                new Advertisement { Id = 2, Name = "Додатково", ClassId = classId },
                new Advertisement { Id = 3, Name = "На завтра", ClassId = classId + 1 },
            };

            advertisementRepositoryMock.Setup(repo => repo.GetAllq()).Returns(advertisementsForClass.AsQueryable());

            // Act
            var result = advertisementService.GetAllAdvertisementsForClassId(classId);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(ad => ad.Id == 1 && ad.Name == "Важливо");
            result.Should().Contain(ad => ad.Id == 2 && ad.Name == "Додатково");
        }

        [Fact]
        public void AddAdvertisement_ShouldInsertAndSave()
        {
            // Arrange
            var advertisementRepositoryMock = new Mock<GenericRepository<Advertisement>>();
            var classRepositoryMock = new Mock<GenericRepository<Class>>();
            var pupilRepositoryMock = new Mock<GenericRepository<Pupil>>();
            var advertisementService = new AdvertisementService(advertisementRepositoryMock.Object, classRepositoryMock.Object, pupilRepositoryMock.Object);

            var advertisement = new Advertisement
            {
                Id = 1,
                Name = "Важливо",
                Description = "Оголошення",
                ClassId = 1
            };

            advertisementRepositoryMock.Setup(repo => repo.Insert(advertisement));
            advertisementRepositoryMock.Setup(repo => repo.Save());

            // Act
            var result = advertisementService.AddAdvertisement(advertisement);

            // Assert
            result.Should().NotBeNull(); 
            result.Id.Should().Be(1); 
            result.Name.Should().Be("Важливо");
        }
    }
}
