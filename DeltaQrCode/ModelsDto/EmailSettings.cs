﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.ModelsDto
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailKey { get; set; }
    }
}
