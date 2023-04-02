namespace TaskIt.Core.Request.Builder
{
    public class CreateStepRequestBuilder: IBuilder<CreateStepRequest>
    {
        private string _title;

        private string _description { get; set; }

        private Guid _taskId { get; set; }
       

        public CreateStepRequestBuilder WithTitle(string title)
        {
           _title = title;
            return this;
        }

        public CreateStepRequestBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CreateStepRequestBuilder WithTaskId(Guid taskId)
        {
            _taskId = taskId;
            return this;
        }

        public CreateStepRequest Build() => new CreateStepRequest
        {
            Title = _title,
            Description = _description,
            TaskId = _taskId
        };
    }
}
