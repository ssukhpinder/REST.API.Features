using System.ComponentModel.DataAnnotations;

namespace REST.API.Features.Models
{
    public class ModelValidationExample
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required", AllowEmptyStrings = false)]
        public string? LastName { get; set; }
    }
}
