using EndpointsSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EndpointsSystem.Domain.Entities
{
    public class Endpoint
    {
        [Key]
        public int EndpointId { get; set; }
        public string EndpointSerialNumber { get; set; }
        public EMeterModelId MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public ESwitchState SwitchState { get; set; }
    }
}
