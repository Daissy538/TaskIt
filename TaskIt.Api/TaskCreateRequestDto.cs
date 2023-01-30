using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace IntegrationTests
{
    public class TaskCreateRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
    }
}