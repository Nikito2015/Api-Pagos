namespace TrylogycWebsite.Common.ServiceRequests.Interfaces
{
    public interface IUpdatePaymentMPRequest
    {
        #region Propiedades
        string preferenceId { get; set; }
        int estado { get; set; }
        string collection { get; set; }
        string merchantOrder { get; set; }
        #endregion
    }
}
