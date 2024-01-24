using System.Security.Cryptography.Pkcs;

namespace WebApplication1.Models.DTO
{
    public class UserDTO
    {
        public string? Name { get; set; }    
        public int? Age { get; set; }
        public string? Email { get; set; }
        public IFormFile? Avatar { get; set; }  
    }
}
