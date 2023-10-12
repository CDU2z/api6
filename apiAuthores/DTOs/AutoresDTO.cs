using apiAuthores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace apiAuthores.DTOs
{
    public class AutoresDTO
    {
        [PrimeraLetraMayuscula]
        [Required(ErrorMessage = "El  campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El  campo {0} no debe tener mas de {1} caracteres")]
        public string Nombres { get; set; }
    }
}
