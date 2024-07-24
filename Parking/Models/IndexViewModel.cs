using System.Collections.Generic;

namespace Parking.Models
{
    public class IndexViewModel
    {
        public VehicleRegistration VehicleRegistration { get; set; }
        public IEnumerable<VehicleRegistration> VehicleRegistrations { get; set; }
    }
}
