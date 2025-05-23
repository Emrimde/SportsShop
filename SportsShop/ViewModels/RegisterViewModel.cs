﻿using System.ComponentModel.DataAnnotations;

namespace SportsShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="The email is required")]
        [EmailAddress(ErrorMessage = "Write correct email!")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage ="This field is required")]
        [Compare("Password", ErrorMessage = "Passwords are't the same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = default!;

    }

}
