namespace DeltaQrCode.ModelsDto
{
    public class SetAnvelopeDto
    {
        public uint Id { get; set; }
        public uint MasinaId { get; set; }
        public uint ClientId { get; set; }
        public string NumeSet { get; set; }
        public int NrBucati { get; set; }
        public string PozitieSet { get; set; }
        public bool Deleted { get; set; }

    }
}
