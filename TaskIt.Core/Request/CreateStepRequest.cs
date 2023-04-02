

using TaskIt.Core.Entities;
using UnitTests;

namespace TaskIt.Core.Request
{
    public class CreateStepRequest
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public Guid TaskId { get; set; }

        public void VerifyData()
        {
            if(string.IsNullOrEmpty(Title)) {
                throw new InvalidStepItemException("Title is missing for this step");
            }else if(TaskId == default) {
                throw new InvalidStepItemException("Step is not coupled to a task");
            }
        }

        public Step GenerateStep()
        {
            return new Step()
            {
                Title = Title,
                Description = Description,
                TaskId = TaskId
            };
        }
    }
}
