﻿
namespace DeltaQrCode.ViewModels.Appointments
{
    public class ConfirmVM
    {

        public ConfirmVM()
        {

        }
        public ConfirmVM(int id, bool confirm)
        {
            Id = id;
            Confirm = confirm;
        }

        public int Id { get; set; }
        public bool Confirm { get; set; }
    }
}
