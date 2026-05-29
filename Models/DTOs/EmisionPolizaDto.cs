using System.ComponentModel.DataAnnotations;

namespace Lafise.ModuloEmision.Api.Models.DTOs 
{ 
    public class EmisionPolizaDto 
    {
        // Datos del cliente 
        [Required(ErrorMessage = "La identificacion es requerida")]
        public int ClienteId { get; set; }
       

        // Datos del vehículo
        [Required(ErrorMessage = "La matricula es requerida")]
        [RegularExpression(@"^[M-Z]\s?\d{3,6}$", ErrorMessage = "La matrícula debe comenzar con una letra, seguida de un espacio opcional y entre 3 a 6 dígitos.")]
        public string Matricula { get; set; } = string.Empty;
        [Required(ErrorMessage = "La marca es requerida")]
        public string Marca { get; set; } = string.Empty;
        [Required(ErrorMessage = "El modelo es requerido")]
        public string Modelo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El año es requerido")]
        public int Anio { get; set; }
        [Required(ErrorMessage = "El valor comercial es requerido")]
        public decimal ValorComercial { get; set; }

        public List<int> CoberturasIds { get; set; } = new List<int>();

    }
}