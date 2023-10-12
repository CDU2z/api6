using System.ComponentModel.DataAnnotations;

namespace apiAuthores.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
