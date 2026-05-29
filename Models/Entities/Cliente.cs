using System.ComponentModel.DataAnnotations;

namespace Lafise.ModuloEmision.Api.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        [Required]
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        [Required]
        public string Identificacion { get; set; } = string.Empty;
        [Required]
        public string Correo { get; set; } = string.Empty;
        public bool EsActivo { get; set; } = true;


    }
}