using CommonTrylogycWebsite.DTO.Extensions;
using CommonTrylogycWebsite.DTO.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace TrylogycWebsite.Common.DTO
{
    public class DTOFactura : IDTOFactura
    {
        public int IdSocio { get; set; }


       public  int IdConexion { get; set; }

        public string NroFactura { get; set; }

        public decimal ImporteFactura { get; set; }

    }
}
