using System.ComponentModel.DataAnnotations;

namespace TaskIt.Api.Dtos.Input
{
    public class TaskCreateRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
    }
}