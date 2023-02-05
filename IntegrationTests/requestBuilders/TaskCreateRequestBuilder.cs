using TaskIt.Api.Dtos.Input;

namespace IntegrationTests
{
    public class TaskCreateRequestBuilder
    {
        public string Title;
        public DateTime? EndDate;

        public TaskCreateRequestBuilder WithTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public TaskCreateRequestBuilder WithEndDate(DateTime? endDate)
        {
            this.EndDate = endDate;
            return this;
        }

        public TaskCreateRequestDto Create()
        {
            return new TaskCreateRequestDto { Title = Title,EndDate = EndDate };
        }
    }
}