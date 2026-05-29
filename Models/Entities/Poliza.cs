using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lafise.ModuloEmision.Api.Models.Entities
{
    public class Poliza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NumeroPoliza { get; set; } = string.Empty;
        [Required]
        public DateTime FechaEmision { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SumaAsegurada { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrimaTotal { get; set; }
        public bool EsActivo { get; set; } = true;

        // Relaciones
        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required]
        public int VehiculoId { get; set; }
        [ForeignKey("VehiculoId")]
        public Vehiculo? Vehiculo { get; set; }

    }
}