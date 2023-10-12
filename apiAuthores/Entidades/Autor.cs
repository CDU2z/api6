using System.ComponentModel.DataAnnotations;

namespace apiAuthores.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        //[PrimeraLetraMayuscula]
        [Required(ErrorMessage = "El  campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El  campo {0} no debe tener mas de {1} caracteres")]
        public string Nombre { get; set; }

        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                    yield return new ValidationResult("La primer letra tiene que estar en mayuscula", new string[] { nameof(Nombre) });
            }
        }
    }
}