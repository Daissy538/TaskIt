using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Text;
using System.Text.Json;

namespace IntegrationTests
{
    public class Tasks : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";

        private readonly WebApplicationFactory<Program> _factory;

        public Tasks(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(TASK_TITLE, "4040-01-25T20:11:42Z")]
        [InlineData(TASK_TITLE, null)]
        public async void Add_A_Task(string title, string? dateTime)
        {
            //Arrange
            var client = _factory.CreateClient();
            
            using StringContent request = new(
                                            JsonSerializer.Serialize(new
                                            {
                                                title = title,
                                                endDate = dateTime
                                            }),
                                            Encoding.UTF8,
                                            "application/json");

            //Act
            var response = await client.PostAsync("Api/Task", request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Can_Not_Add_A_Task_Without_Title(string title)
        {
            var client = _factory.CreateClient();

            using StringContent request = new(
                                            JsonSerializer.Serialize(new
                                            {
                                                title = title
                                            }),
                                            Encoding.UTF8,
                                            "application/json");

            //Act
            var response = await client.PostAsync("Api/Task", request);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}