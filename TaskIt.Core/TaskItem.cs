namespace UnitTests
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

        public string Title { get; }
        public DateTime? EndDate { get; }
    }
}