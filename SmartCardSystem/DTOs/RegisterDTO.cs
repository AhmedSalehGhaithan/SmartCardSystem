using System.ComponentModel.DataAnnotations;
namespace SmartCardSystem.DTOs
{
    public class RegisterDTO :LoginDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
