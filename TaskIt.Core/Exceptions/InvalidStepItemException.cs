using System.Runtime.Serialization;

namespace UnitTests
{
    public class InvalidStepItemException : Exception
    {
        public InvalidStepItemException()
        {
        }

        public InvalidStepItemException(string? message) : base(message)
        {
        }

        public InvalidStepItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}