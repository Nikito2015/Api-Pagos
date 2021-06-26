using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CommonTrylogycWebsite.DTO.Interfaces
{

    /// <summary>
    /// Interface for a user.
    /// </summary>
    public interface IDTOFactura 
    {
        #region Properties


        int IdSocio { get; set; }


        int IdConexion { get; set; }

        string NroFactura { get; set; }

        decimal ImporteFactura { get; set; }

        #endregion

    }
}
