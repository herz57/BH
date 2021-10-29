using System.ComponentModel.DataAnnotations;

namespace BH.Common.Dtos
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
