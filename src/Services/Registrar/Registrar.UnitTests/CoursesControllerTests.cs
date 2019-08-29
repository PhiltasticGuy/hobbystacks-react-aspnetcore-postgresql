using Moq;
using Registrar.Api.Controllers;
using Registrar.Api.Data;
using System.Collections.Generic;
using Xunit;

namespace Registrar.UnitTests
{
    public class CoursesControllerTests
    {
        [Fact]
        public async void GetCourses_DatabaseEmpty_ReturnsEmpty()
        {
            // Arrange
            var expectedCourses = new List<Course>();

            var fakeCoursesRepository = new Mock<ICourseRepository>();
            fakeCoursesRepository
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(expectedCourses);

            var coursesController = new CoursesController(fakeCoursesRepository.Object);

            // Act
            var actionResult = await coursesController.GetCourses();

            // Assert
            Assert.NotNull(actionResult);

            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("Introduction to Programming")]
        [InlineData("Introduction to Computer Science")]
        [InlineData("History of Epidemiology")]
        public async void GetCourses_DatabaseNonEmpty_ReturnsAllRecords(string titleEn)
        {
            // Arrange
            var expectedCourses =
                new List<Course>()
                {
                    new Course()
                    {
                        TitleEn = "Introduction to Programming"
                    },
                    new Course()
                    {
                        TitleEn = "Introduction to Computer Science"
                    },
                    new Course()
                    {
                        TitleEn = "History of Epidemiology"
                    }
                };

            var fakeCoursesRepository = new Mock<ICourseRepository>();
            fakeCoursesRepository
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(expectedCourses);

            var coursesController = new CoursesController(fakeCoursesRepository.Object);

            // Act
            var actionResult = await coursesController.GetCourses();

            // Assert
            Assert.NotNull(actionResult);

            var result = actionResult.Value;
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedCourses.Count, result.Count);
            Assert.Contains(result, c => c.TitleEn == titleEn);
        }
    }
}
