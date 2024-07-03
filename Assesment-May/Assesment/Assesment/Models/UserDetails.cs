using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Assesment.Models
{
    public class UserDetails : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        //[Required(ErrorMessage = "Please enter Password")]

       // public string Password{ get; set; }


        //[Required(ErrorMessage = "Please enter Age")]
        //[Required(ErrorMessage = "Please enter Age")]

        public int Age { get; set; }

        //[Required(ErrorMessage = "Please enter Phone")]
        //[StringLength(10,MinimumLength =10,ErrorMessage ="The phone number must be exactly 10 digits")]
        public string Phone { get; set; }
        //public bool Rememberme { get; set; }
        //[Required(ErrorMessage = "Please enter Name")]

        //public string UserName { get; set;}
        //[Required(ErrorMessage = "Please enter Email")]
        //[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address.")]
        //public string Email {  get; set;}

    }
}

