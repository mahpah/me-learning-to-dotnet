using System;
using System.Linq;
using Xunit;
using superweb.Controllers;
using superweb.Models;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace superweb.test
{
    public class TodoControllerTest
    {
        [Fact]
        public void GetReturnListOfTodo()
        {
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(GetTestTodoList());
            var controller = new TodoController(mockRepo.Object);

            var result = controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Equal(1, returnedResult.Count());
        }

        [Fact]
        public void GetByIdShouldSendNotFoundWhenTodoItemNotFound()
        {
            var testId = 2;
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.Find(testId))
                .Returns((TodoItem)null);

            var controller = new TodoController(mockRepo.Object);
            var result = controller.GetById(testId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_SendBaedRequest_WhenModelInvalid()
        {
        //Given
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(GetTestTodoList())
                .Verifiable();
            var controller = new TodoController(mockRepo.Object);

        //When
            controller.ModelState.AddModelError("Name", "Required");
            var result = controller.Create(new TodoItem());

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
