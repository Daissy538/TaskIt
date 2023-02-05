using System.Text.Json.Serialization;

namespace TaskIt.Api.Dtos.Output
{
    public class TaskItemDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }
}
