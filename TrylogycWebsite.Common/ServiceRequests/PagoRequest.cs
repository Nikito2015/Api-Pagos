using System.Runtime.Serialization;
using CommonTrylogycWebsite.ServiceRequests.Interfaces;

namespace CommonTrylogycWebsite.ServiceRequests
{

    /// <summary>
    /// A login request.
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.Interfaces.IPagoRequest" />

    public class PagoRequest : BaseRequest, IPagoRequest
    {

        #region Public Properties

       
        public int idSocio { get; set; }


        public int idConexion { get; set; }

        public string numFact { get; set; }

              
        public decimal importe { get; set; }


        #endregion

        #region Public Methdos

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool IsValid()
        {
            return ( idSocio!=0 ||
                        !string.IsNullOrEmpty(numFact) );
        }
        #endregion
    }
}