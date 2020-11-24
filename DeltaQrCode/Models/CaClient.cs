using System;
using System.Collections.Generic;

namespace DeltaQrCode.Models
{
    public class CaClient
    {
        public int Id { get; set; }
        public string IdManopera { get; set; }
        public string NrFisa { get; set; }
        public string TipFactura { get; set; }
        public string NrFactura { get; set; }
        public DateTime DataFacturare { get; set; }
        public int Nefacturat { get; set; }
        public DateTime DataInsert { get; set; }
        public DateTime DataInstalari { get; set; }
        public DateTime DataExpirareAbonament { get; set; }
        public DateTime DataInitiala { get; set; }
        public string NrFacturaAbonament { get; set; }
        public int ZileExpirareAbonament { get; set; }
        public string NumeClient { get; set; }
        public string ReprezentantClient { get; set; }
        public string ReprezentantClientTelefon { get; set; }
        public string ReprezentantClientMail { get; set; }
        public string NrMasina { get; set; }
        public string SerieSasiu { get; set; }
        public int AnFabricatie { get; set; }
        public string MarcaMasina { get; set; }
        public string TipAuto { get; set; }
        public float KmBord { get; set; }
        public string LocatieMontaj { get; set; }
        public float KmEfectuati { get; set; }
        public string Vin { get; set; }
        public string SeriaSim { get; set; }
        public string NrTelefon { get; set; }
        public int Custodie { get; set; }
        public string SerieGps { get; set; }
        public string NoteInstalare { get; set; }
        public string Instalator { get; set; }
        public string TipServiciu { get; set; }
        public string NrBucati { get; set; }
        public string TipAparat { get; set; }
        public string UserAccount { get; set; }
        public int FirmaPrestatoare { get; set; }
        public string Stare { get; set; }
        public float PerioadaContractuala { get; set; }
        public float CostAbonament { get; set; }
        public string TipVanzare { get; set; }
    }
}
