using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class RegisterDTO
    {
            [Required(ErrorMessage = "The email is required")]
            [EmailAddress(ErrorMessage = "Write correct email!")]
            public string Email { get; set; } = default!;

            [Required(ErrorMessage = "Password is required")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = default!;

            [Required(ErrorMessage = "First name is required")]
            public string FirstName { get; set; } = default!;

            [Required(ErrorMessage = "Last name is required")]
            public string LastName { get; set; } = default!;

            [Required(ErrorMessage = "This field is required")]
            [Compare("Password", ErrorMessage = "Passwords are't the same")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = default!;
        }
    }


