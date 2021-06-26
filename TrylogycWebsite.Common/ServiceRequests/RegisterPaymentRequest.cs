using System.Collections.Generic;
using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;
using TrylogycWebsite.Common.DTO;
using System.Linq;

namespace CommonTrylogycWebsite.ServiceRequests
{
    public class RegisterPaymentRequest : BaseRequest, IRegisterPaymentRequest
    {
        #region Property
        public string nroFactura { get; set; }
        public decimal importe
        {
            get 
            { 
                return Facturas.Sum(x => x.ImporteFactura);
            } 
        }
        //public string preference { get; set; }
        //public int idSocio { get; set; }
       // public int idConexion { get; set; }
        public int idMedioPago { get; set; }

        public List<DTOFactura> Facturas { get; set; }
        #endregion

        #region Métodos
        public override bool IsValid()
        {
            return true;
        }
        #endregion
    }
}
