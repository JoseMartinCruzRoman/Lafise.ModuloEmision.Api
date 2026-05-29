using System.ComponentModel.DataAnnotations;

namespace Lafise.ModuloEmision.Api.Models.Entities
{
    public class Cobertura
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public decimal Tasa { get; set; }
        public bool EsActivo { get; set; } = true;
    }
}