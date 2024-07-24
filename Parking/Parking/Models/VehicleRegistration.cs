using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class VehicleRegistration
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        [StringLength(10)]
        [DisplayName("Placa")]
        public string? LicensePlate { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Horário de Chegada")]
        public DateTime Enter { get; set; }
        [DisplayName("Horário de Saída")]
        public DateTime? Exit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Valor a pagar")]
        public decimal? AmountCharged { get; set; }
    }
}
