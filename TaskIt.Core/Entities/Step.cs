namespace TaskIt.Core.Entities
{
    public class Step
    {
        public Step()
        {
        }

        public Step(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}