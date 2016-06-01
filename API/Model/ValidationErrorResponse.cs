using System.Collections.Generic;

namespace API.Model
{
    public class ValidationErrorResponse
    {
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}