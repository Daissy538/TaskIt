﻿using TaskIt.Core.Exceptions;

namespace TaskIt.Core.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public DateTime? EndDate { get; set; }

        public void VerifyEndDate(DateTime currentDate)
        {
            if (EndDate.HasValue)
            {
                var endDateInThePast = EndDate.Value.Date < currentDate.Date;

                if (endDateInThePast)
                {
                    throw new InvalidTaskItemException("The end date is in the past");
                }
            }
        }
    }
}
