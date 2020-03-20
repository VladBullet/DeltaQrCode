using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DeltaQrCode.Data;
using DeltaQrCode.Models;
using DeltaQrCode.ViewModels;

namespace DeltaQrCode.Services
{
    public class QrService : IQrService
    {
        private ApplicationDbContext dataService;
        private const int zero = 0;
        private const int unu = 1;
        private const string instalator = "Aplicatie QRCode";
        private const string locatieMontaj = "Delta";
        private const string noteInstalare = "SPALATORIE";
        public QrService(ApplicationDbContext dbData)
        {
            dataService = dbData;
        }

        public bool SaveQrCode(Bitmap img)
        {
            return true;
        }

        public Result<QrCodeContentViewModel> SaveOperation(QrCodeContentViewModel model)
        {
            try
            {
                var date = DateTimeOffset.FromUnixTimeMilliseconds(model.DateTimeTicks).DateTime;
                var str = model.QrCodeVal.Split(",");
                var nrMasina = str[0].Trim();
                var flota = str[1].Trim();
                var selectie = (Operatiune)model.Selection;
                Models.CaClient toSave = new Models.CaClient
                {
                    DataInsert = date,
                    NrMasina = nrMasina,
                    NumeClient = flota,
                    TipAparat = selectie.ToString(),
                    TipServiciu = "cosmetica",
                    UserAccount = "cosmetica",
                    FirmaPrestatoare = 4,

                    DataInstalari = date,
                    //DataExpirareAbonament = null,
                    //DataFacturare = null,
                    //DataInitiala = null,

                    // nefolosite dar obligatorii
                    IdManopera = zero.ToString(),
                    Instalator = instalator,
                    LocatieMontaj = locatieMontaj,
                    MarcaMasina = string.Empty,
                    NoteInstalare = noteInstalare,
                    NrBucati = unu.ToString(),
                    NrFacturaAbonament = string.Empty,
                    NrFisa = string.Empty,
                    AnFabricatie = zero,
                    CostAbonament = (float)zero,
                    Custodie = zero,
                    KmBord = (float)zero,
                    KmEfectuati = (float)zero,
                    NrFactura = string.Empty,
                    SeriaSim = string.Empty,
                    SerieGps = string.Empty,
                    SerieSasiu = string.Empty,
                    NrTelefon = string.Empty,
                    ReprezentantClient = string.Empty,
                    ReprezentantClientMail = string.Empty,
                    ReprezentantClientTelefon = string.Empty,
                    Vin = string.Empty,
                    TipAuto = string.Empty,
                    TipFactura = string.Empty,
                    TipVanzare = string.Empty,
                    Nefacturat = zero,
                    Stare = string.Empty,
                    PerioadaContractuala = (float)zero,
                    ZileExpirareAbonament = zero

                };

                dataService.CaClient.Add(toSave);
                var response = dataService.SaveChangesAsync();
                return Result<QrCodeContentViewModel>.ResultOk(model);
            }
            catch (Exception e)
            {
                return Result<QrCodeContentViewModel>.ResultError(model, e);
            }

        }
    }
}
