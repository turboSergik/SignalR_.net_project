//using AutoMapper;
//using JobSolution.API.Controllers;
//using JobSolution.Domain.Auth;
//using JobSolution.Domain.Entities;
//using JobSolution.DTO.DTO;
//using JobSolution.Services.Concrete;
//using JobSolution.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Org.BouncyCastle.Asn1.Cms;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace JobSolution.Tests
//{

//    public class JobControllerTest
//    {

//        public List<Job> jobList = new List<Job>() {
//            new Job(){ Id=1, AuthorId=1, CategoryId=1, City="Chisinau"},
//            new Job(){ Id=2, AuthorId=2, CategoryId=2, City="Orhei"},
//            new Job(){ Id=3, AuthorId=3, CategoryId=3, City="Iasi"},
//            new Job(){ Id=4, AuthorId=1, CategoryId=4, City="Orhei"},
//            new Job(){ Id=5, AuthorId=1, CategoryId=5, City="Balti"},

//        };


//        [Fact]
//        public void GetAllJobsOkTest()
//        {
//             Arrange
//            var mockRepository = new Mock<IJobService>();
//            var mockIMapper = new Mock<IMapper>();
//            var controller = new JobController(mockRepository.Object, mockIMapper.Object);

//             Act
//            Task<IActionResult> ActionResult = controller.GetAll();
//            OkObjectResult contentResult = (OkObjectResult)ActionResult.Result;

//             Assert
//             mockRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Job>() { });
//            mockRepository.Verify(x => x.GetAll(), Times.Once);
//            Assert.NotNull(contentResult);
//            Assert.Equal(200, contentResult.StatusCode);
//        }


//        [Fact]
//        public void DeleteUncorrectIdTest()
//        {
//            Arrange
//            var mockRepository = new Mock<IJobService>();
//            var mockIMapper = new Mock<IMapper>();
//            var controller = new JobController(mockRepository.Object, mockIMapper.Object);
//            int id = -5; // value

//            Act
//            mockRepository.Setup(x => x.Remove(id));

//            Task<IActionResult> ActionResult = controller.Delete(id);
//            NotFoundResult contentResult = (NotFoundResult)ActionResult.Result;

//            Assert
//            Assert.NotNull(contentResult);
//            Assert.Equal(404, contentResult.StatusCode);
//            mockRepository.Verify(x => x.Remove(id), Times.Never());
//        }


//        [Fact]
//        public void GetByIdTest()
//        {
//            Arrange
//            var mockRepository = new Mock<IJobService>();
//            var mockIMapper = new Mock<IMapper>();
//            var controller = new JobController(mockRepository.Object, mockIMapper.Object);
//            int id = 1;

//            mockRepository.Setup(x => x.GetByID(id)).ReturnsAsync(new Job());

//            Check
//            Task<IActionResult> ActionResult = controller.GetById(id);
//            OkObjectResult contentResult = (OkObjectResult)ActionResult.Result;

//            Assert
//            Assert.NotNull(contentResult);
//            Assert.Equal(200, contentResult.StatusCode);
//        }


//        [Fact]
//        public void DeleteCorrectIdTest()
//        {
//            Arrange
//            var mockRepository = new Mock<IJobService>();
//            var mockIMapper = new Mock<IMapper>();
//            var controller = new JobController(mockRepository.Object, mockIMapper.Object);
//            int id = 10000; // value bigger the base contains or correct value

//            Check
//            Task<IActionResult> ActionResult = controller.Delete(id);
//            try
//            {
//                NotFoundResult contentResult = (NotFoundResult)ActionResult.Result;

//                Assert

//                mockRepository.Setup(x => x.Remove(id)).Returns(Task.CompletedTask);

//                Assert.NotNull(contentResult);
//                Assert.Equal(404, contentResult.StatusCode);
//            }
//            catch (Exception ex)
//            {
//                OkObjectResult contentResult = (OkObjectResult)ActionResult.Result;

//                Assert

//                mockRepository.Setup(x => x.Remove(id)).Returns(Task.CompletedTask);
//                Assert.NotNull(contentResult);
//                Assert.Equal(200, contentResult.StatusCode);
//            }
//        }


//        [Fact]
//        public void PostCorrectModelTest()
//        {

//            var mockRepository = new Mock<IJobService>().Object;
//            var mockIMapper = new Mock<IMapper>().Object;
//            var controller = new JobController(mockRepository, mockIMapper);
//            var sendValue = new JobDTO()
//            {
//                Title = "test title",
//                City = "test city",
//                Contact = "test contact",
//                AuthorId = 1,
//                CategoryId = 1
//            };


//              Task<IActionResult> ActionResult = controller.Post(sendValue);
//             OkResult contentResult = (OkResult)ActionResult.Result;

//            Assert.NotNull(contentResult);
//            Assert.Equal(200, contentResult.StatusCode);
//        }

//        [Fact]
//        public void UpdateCorrectModelTest()
//        {

//            var mockRepository = new Mock<IJobService>().Object;
//            var mockIMapper = new Mock<IMapper>().Object;
//            var controller = new JobController(mockRepository, mockIMapper);
//            var sendValue = new JobDTO()
//            {
//                Title = "test title",
//                City = "test city",
//                Contact = "test contact",
//                AuthorId = 1,
//                CategoryId = 1,
//                Id = 0
//            };


//            Task<IActionResult> ActionResult = controller.Update(sendValue);
//            OkResult contentResult = (OkResult)ActionResult.Result;

//            Assert.NotNull(contentResult);
//            Assert.Equal(200, contentResult.StatusCode);
//        }

//        [Fact]

//        public void UpdateUnсorrectModelTest()
//        {

//            var mockRepository = new Mock<IJobService>();
//            var mockIMapper = new Mock<IMapper>();
//            var controller = new JobController(mockRepository.Object, mockIMapper.Object);
//            var sendValue = new JobDTO()
//            {
//                Title = "test title not in table",
//                City = "test city not in table",
//                Contact = "test contact not in table",
//                AuthorId = 5,
//                CategoryId = 55,
//                Id = 114
//            };

//             Task<IActionResult> ActionResult = controller.Update(sendValue);

//            try
//            {
//                NotFoundResult contentResult = (NotFoundResult)ActionResult.Result;

//                Assert.NotNull(contentResult);
//                Assert.Equal(404, contentResult.StatusCode);
//            }
//            catch (Exception ex)
//            {

//                OkResult contentResult = (OkResult)ActionResult.Result;

//                Assert.NotNull(contentResult);
//                Assert.Equal(200, contentResult.StatusCode);

//            }

//        }
//    }
//}
