using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Models
{
    [Table("Autores")]
    public class Autores
    {
        [Key]
        public int AutorID { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        // Relación 1 a muchos
        public ICollection<Libros> Libros { get; set; } = new List<Libros>();
    }

    [Table("Libros")]
    public class Libros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 👈 importante
        public int ID { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un autor")]
        public int AutorID { get; set; }

        [ForeignKey("AutorID")]
        public Autores? Autor { get; set; } = null!;
    }

}
