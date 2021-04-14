
namespace DeltaQrCode.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Contact { get => @"Please use this contact: email: buletevlad@yahoo.com phone: +40 748.885.529"; }
    }
}