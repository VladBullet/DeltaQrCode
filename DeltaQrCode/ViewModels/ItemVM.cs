using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ViewModels
{
    public class ItemVM
    {
        public uint Id { get; set; }
        public string Text { get; set; }

        public ItemVM(uint id, string text)
        {
            Id = id;
            Text = text;
        }

        public ItemVM()
        {

        }
    }
}
