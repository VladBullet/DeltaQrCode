
namespace DeltaQrCode.Models
{
    public partial class CaLogOperatiune
    {
        public uint Id { get; set; }
        public int? AppointmentId { get; set; }
        public bool AjunsLaTimp { get; set; }
        public int? OperatiuneId { get; set; }
    }
}
