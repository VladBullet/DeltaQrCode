using System;
using System.Collections.Generic;

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
