using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Model
    {
        public string VIN1_10 { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? PostalCode { get; set; }
        public int? ModelYear { get; set; }
        public string Make { get; set; }
        public string VModel { get; set; }
        public string ElectricVehicleType { get; set; }
        public string CleanAlternativeFuelVehicleCAFVEligibility { get; set; }
        public int? ElectricRange { get; set; }
        public int? BaseMSRP { get; set; }
        public int? LegislativeDistrict { get; set; }
        public int DOLVehicleID { get; set; }
        public string VehicleLocation { get; set; }
        public string ElectricUtility { get; set; }
        public long? num2020CensusTract { get; set; }
    }
}
