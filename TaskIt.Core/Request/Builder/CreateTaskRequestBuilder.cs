namespace TaskIt.Core.Request.Builder
{
    public class CreateTaskRequestBuilder: IBuilder<CreateTaskRequest>
    {
        private string? Title;
        private DateTime? EndDate;


        public CreateTaskRequestBuilder WithTitle(string title)
        {
           this.Title = title;
            return this;
        }

        public CreateTaskRequestBuilder WithEndDate(DateTime endDate)
        {
            this.EndDate = endDate;
            return this;
        }

        public CreateTaskRequest Build() => new CreateTaskRequest
        {
            Title = Title,
            EndDate = EndDate
        };
    }
}
