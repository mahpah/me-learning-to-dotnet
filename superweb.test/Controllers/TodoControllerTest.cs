using System;
using System.Linq;
using Xunit;
using superweb.Controllers;
using superweb.Models;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace superweb.test
{
    public class TodoControllerTest
    {
        [Fact]
        public async Task GetReturnListOfTodo()
        {
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(GetTestTodoList()));
            var controller = new TodoController(mockRepo.Object);

            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Equal(1, returnedResult.Count());
        }

        [Fact]
        public async void GetByIdShouldSendNotFoundWhenTodoItemNotFound()
        {
            var testId = 2;
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.Find(testId))
                .Returns(Task.FromResult((TodoItem)null));

            var controller = new TodoController(mockRepo.Object);
            var result = await controller.GetById(testId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_SendBaedRequest_WhenModelInvalid()
        {
        //Given
            var mockRepo = new Mock<ITodoRepository>();
            var controller = new TodoController(mockRepo.Object);

        //When
            controller.ModelState.AddModelError("Name", "Required");
            var result = await controller.Create(new TodoItem());

        //Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private IEnumerable<TodoItem> GetTestTodoList()
        {
            var list = new List<TodoItem>();
            list.Add(new TodoItem() {
                Name = "Walk the dog",
                IsComplete = false
            });

            return list;
        }
    }
}
