using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lafise.ModuloEmision.Api.Models.Entities
{
    public class PolizaCobertura
    {
        [Key]
        public int Id { get; set; }
       
        // Relaciones
        [Required]
        public int PolizaId { get; set; }
        [ForeignKey("PolizaId")]
        public Poliza? Poliza { get; set; }
        [Required]
        public int CoberturaId { get; set; }
        [ForeignKey("CoberturaId")]
        public Cobertura? Cobertura { get; set; }
    }
}