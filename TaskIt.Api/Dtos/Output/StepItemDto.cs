using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskIt.Api.Dtos.Output
{
    public class StepItemDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}