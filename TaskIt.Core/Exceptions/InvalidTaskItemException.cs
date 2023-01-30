namespace TaskIt.Core.Exceptions
{
    public class InvalidTaskItemException: Exception
    {
        public InvalidTaskItemException():base()
        {
               
        }

        public InvalidTaskItemException(string message)
        : base(message)
        {
        }

        public InvalidTaskItemException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}