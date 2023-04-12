using System.ComponentModel.DataAnnotations;
using TaskIt.Core.Entities;

namespace TaskIt.Api.Dtos.Input
{
    public class StepCreatedRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [StringLength(400)]
        public string Description { get; set; }
        [Required]
        public Guid TaskId { get; set; }

    }
}