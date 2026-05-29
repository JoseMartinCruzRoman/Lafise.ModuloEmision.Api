using System.ComponentModel.DataAnnotations;

namespace Lafise.ModuloEmision.Api.Models.Entities
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Matricula { get; set; } = string.Empty;
        [Required]
        public string Marca { get; set; } = string.Empty;
        [Required]
        public string Modelo { get; set; } = string.Empty;
        [Required]
        public int Anio { get; set; }
        [Required]
        public decimal ValorComercial { get; set; }
        public bool EsActivo { get; set; } = true;
    }
}