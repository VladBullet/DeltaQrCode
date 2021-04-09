using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class AvailableIntervalDto
    {
        public AvailableIntervalDto() { }
        public AvailableIntervalDto(bool selectedIsAvailable, List<TimeSpan> availableSpans)
        {
            SelectedIsAvailable = selectedIsAvailable;
            AvailableSpans = availableSpans;
        }
        public bool SelectedIsAvailable { get; set; }

        public List<TimeSpan> AvailableSpans { get; set; }
    }
}
