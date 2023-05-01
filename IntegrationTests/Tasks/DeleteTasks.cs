using IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Tasks
{
    public class DeleteTasks : TestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        public DeleteTasks(WebApplicationFactory<Program> factory): base(factory)
        {
        }

        public async ValueTask DisposeAsync()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Fact]
        public async Task Delete_Task_By_Id()
        {
            //Arrange
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            //Act
            var response = await _client.DeleteAsync($"{TASK_URL}/{createdTaskId}");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Can_Not_Delete_Task_That_Does_Not_Exist()
        {
            //Arrange
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            //Act
            var response = await _client.DeleteAsync($"{TASK_URL}/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Task_With_Steps()
        {
            //Arrange
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);
            var response = await _client.DeleteAsync($"{TASK_URL}/{createdTaskId}");



            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
