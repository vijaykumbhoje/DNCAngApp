using System.ComponentModel.DataAnnotations;

namespace DNCAngApp.API.Dtos
{
    public class UserForRegistrationDto
    {
        [Required]
        [StringLength(10, MinimumLength=6, ErrorMessage="USername length must be between 6 to 10 characters")]
        public string Username { get; set; }
        
        [Required]
       [StringLength(10, MinimumLength=6, ErrorMessage="USername length must be between 6 to 10 characters")]
        public string Password { get; set; }
    }
}