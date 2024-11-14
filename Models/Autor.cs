using System.ComponentModel.DataAnnotations;

namespace Juan.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Falta Apellido")]
        public string Apellido { get; set; }

        public DateOnly FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Biografia { get; set; }
    }
}