

namespace CommonTrylogycWebsite.ServiceRequests.Interfaces
{

    /// <summary>
    /// Interface for a Ipago request.
    /// </summary>
    /// <seealso cref="CommonTrylogycWebsite.ServiceRequests.Interfaces.IBaseRequest" />
    public interface IPagoRequest : IBaseRequest
    {
        //ByVal idSocio As Int32, ByVal idConexion As Int32, ByVal numFact As String, ByVal importe As Decimal
        #region Properties

        /// <summary>
        /// Gets or sets the idSocio ..
        /// </summary>
        /// <value>
        /// </value>
        int idSocio { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        int idConexion { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string numFact { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        decimal importe { get; set; }

        #endregion
    }
}