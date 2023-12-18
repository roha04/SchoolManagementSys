using Moq;
using FluentAssertions;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;
using DayOfWeek = WPFScholifyApp.DAL.DBClasses.DayOfWeek;

namespace UnitTests
{
    public class ScheduleServiceTests
    {
        [Fact]
        public void GetAllSchedulesForSubjectId_ShouldReturnFilteredSchedules()
        {
            // Arrange
            var subjectId = 1;
            var mockRepository = new Mock<GenericRepository<Schedule>>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            var schedules = new List<Schedule>
            {
                new Schedule { SubjectId = subjectId, Subject = new Subject{ Id = 1 } }, 
                new Schedule { SubjectId = 2, Subject = new Subject{ Id = 2 } },
                new Schedule { SubjectId = 3, Subject = new Subject{ Id = 3 } }
            };

            mockRepository.Setup(r => r.GetAllq()).Returns(schedules.AsQueryable());

            // Act
            var result = scheduleService.GetAllSchedulesForSubjectId(subjectId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.All(s => s.SubjectId == subjectId).Should().BeTrue();
        }

        [Fact]
        public void GetDatesBySubjectId_ShouldReturnDistinctDayOfWeeks()
        {
            // Arrange
            var subjectId = 1;
            var mockRepository = new Mock<GenericRepository<Schedule>>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            var schedules = new List<Schedule>
            {
                new Schedule { SubjectId = subjectId, DayOfWeek = new DayOfWeek { Id = 1, Day = "Понеділок" } },
                new Schedule { SubjectId = 2, DayOfWeek = new DayOfWeek { Id = 2, Day = "Вівторок" } },
                new Schedule { SubjectId = subjectId, DayOfWeek = new DayOfWeek { Id = 3, Day = "Середа" } },
            };

            mockRepository.Setup(repo => repo.GetAllq()).Returns(schedules.AsQueryable());

            // Act
            var result = scheduleService.GetDatesBySubjectId(subjectId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetDatesByClassId_ShouldReturnDistinctDayOfWeeks()
        {
            // Arrange
            var classId = 1;
            var mockRepository = new Mock<GenericRepository<Schedule>>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            var schedules = new List<Schedule>
            {
                new Schedule { ClassId = classId, DayOfWeek = new DayOfWeek { Id = 1, Day = "Понеділок" } },
                new Schedule { ClassId = 2, DayOfWeek = new DayOfWeek { Id = 2, Day = "Вівторок" } },
                new Schedule { ClassId = classId, DayOfWeek = new DayOfWeek { Id = 3, Day = "Середа" } },
            };

            mockRepository.Setup(repo => repo.GetAllq()).Returns(schedules.AsQueryable());

            // Act
            var result = scheduleService.GetDatesByClassId(classId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
