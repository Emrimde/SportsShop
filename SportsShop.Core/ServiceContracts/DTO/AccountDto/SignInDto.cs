using System.ComponentModel.DataAnnotations;

namespace SportsShop.Core.ServiceContracts.DTO.AccountDto;
public class SignInDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    public override string ToString()
    {
        return $"{{{nameof(Email)}={Email}, {nameof(Password)}={Password}}}";
    }
}
