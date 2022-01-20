﻿using CommonTrylogycWebsite.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTrylogycWebsite.DTO
{
    public class DTOBalance : IDTOBalance
    {
        public int AssociateId {get;set; }
        public int ConnectionId {get;set; }
        public string Period {get;set; }
        public string InvoiceGroup {get;set; }
        public string InvoiceLetter {get;set; }
        public string InvoicePoint {get;set; }
        public string InvoiceNumber {get;set; }
        public string InvoiceDate {get;set; }
        public string InvoiceExpirationDate {get;set; }
        public string InvoiceAmmount {get;set; }
        public string InvoiceTrackingNumber {get;set; }
        public bool Paid {get;set; }
        public string codigoBarra { get; set; }
    }
}
