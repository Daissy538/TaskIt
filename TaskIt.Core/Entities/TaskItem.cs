using System.ComponentModel.DataAnnotations;

namespace TaskIt.Core.Entities
{
    public class TaskItem
    {
        public TaskItem(string title)
        {
            Title = title;
        }

        public TaskItem(string title, DateTime? endDate)
        {
            Title = title;
            EndDate = endDate;
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Step> Steps { get; set; } = new List<Step>();
    }
}