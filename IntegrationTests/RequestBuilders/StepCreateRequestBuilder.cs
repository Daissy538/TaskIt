using TaskIt.Api.Dtos.Input;

namespace IntegrationTests
{
    public class StepCreateRequestBuilder
    {
        public string Title;
        public string Description;
        public Guid TaskId;

        public StepCreateRequestBuilder WithTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public StepCreateRequestBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public StepCreateRequestBuilder WithTask(Guid taskId)
        {
            this.TaskId = taskId;
            return this;
        }

        public StepCreatedRequestDto Create()
        {
            return new StepCreatedRequestDto { Title = Title, Description  = Description, TaskId = TaskId };
        }
    }
}