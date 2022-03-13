namespace web.Models;

public class ValidateFieldViewModelOutput
{
        public IEnumerable<string> Errors { get; set; }

        public ValidateFieldViewModelOutput(IEnumerable<string> errors)
        {
                Errors = errors;
        }
}
