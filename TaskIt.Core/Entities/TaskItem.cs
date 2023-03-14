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
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
    }
}