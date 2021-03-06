using System.ComponentModel.DataAnnotations;

namespace MyHealthNotebook.Entities.Dtos.Incoming
{
    public class UserRegistrationRequestDto : IUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}