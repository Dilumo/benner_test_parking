using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class PriceTable
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Start Surveillance is required")]
        [Display(Name = "Start Surveillance")]
        public DateTime StartSurveillance { get; set; }

        [Required(ErrorMessage = "End Surveillance is required")]
        [Display(Name = "End Surveillance")]
        public DateTime EndSurveillance { get; set; }

        [Required(ErrorMessage = "Initial Hourly Rate is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Initial Hourly Rate")]
        public decimal ItialHourlyRate { get; set; }

        [Required(ErrorMessage = "Final Hourly Rate is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Final Hourly Rate")]
        public decimal FinalHourlyRate { get; set; }
    }
}
