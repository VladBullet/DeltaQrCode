using System;

namespace DeltaQrCode.Models
{
    public partial class PasOperatiune
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Pas { get; set; }
        public string SavedData { get; set; }
        public int OperatiuneId { get; set; }
        public DateTime InsertedDate { get; set; }
    }
}
