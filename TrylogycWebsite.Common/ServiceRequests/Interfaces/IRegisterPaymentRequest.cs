using System.Collections.Generic;
using TrylogycWebsite.Common.DTO;

namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{
    public interface IRegisterPaymentRequest
    {
        #region Properties
        string nroFactura { get; set; }
        decimal importe { get; }
        //string preference { get; set; }
        //int idSocio { get; set; }
        //int idConexion { get; set; }
        int idMedioPago { get; set; }
        
        List<DTOFactura> Facturas { get; set; }
        #endregion
    }
}
