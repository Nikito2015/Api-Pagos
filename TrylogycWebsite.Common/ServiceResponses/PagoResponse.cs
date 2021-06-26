using System.Runtime.Serialization;
using CommonTrylogycWebsite.Models;

namespace CommonTrylogycWebsite.ServiceResponses
{

    /// <summary>
    /// Login Response class.
    /// </summary>
    
    public class PagoResponse : BaseResponse
    {

        #region Public Properties

        
        public bool tienePagos { get; set; }


        #endregion
    }
}