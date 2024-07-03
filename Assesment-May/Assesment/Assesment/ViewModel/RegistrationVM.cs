using System.ComponentModel.DataAnnotations;

namespace Assesment.ViewModel
{
    public class RegistrationVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter Age")]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number")]

        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter Phone")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="The phone number must be exactly 10 digits")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter Password")]

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{8,}$",
      ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one special character, and one digit")]

        
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter Name")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "The email field must be a valid email address in the format: name@example.com")]


        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
