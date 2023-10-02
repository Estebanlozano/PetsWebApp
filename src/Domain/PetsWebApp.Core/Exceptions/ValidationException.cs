namespace PetsWebApp.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public ValidationException(string error)
        {
            Errors = new List<string>
            {
                error
            };
        }

        public ValidationException(IEnumerable<string> errors) 
        { 
            Errors = errors;
        }
    }
}
