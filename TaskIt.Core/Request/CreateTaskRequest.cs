namespace TaskIt.Core.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
