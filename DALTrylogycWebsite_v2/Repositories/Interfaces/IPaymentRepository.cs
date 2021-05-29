﻿using CommonTrylogycWebsite.DTO.Interfaces;
using DALTrylogycWebsite.DALResponses.Interfaces;
using System;

namespace DALTrylogycWebsite.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        #region Methods

        /// <summary>
        /// Registers Payments.
        /// </summary>
        /// <param name="nroFactura">The nroFactura.</param>
        /// <param name="importe">The importe.</param>
        /// <param name="idSocio">The idSocio.</param>
        /// <param name="idConexion">The idConexion.</param>
        /// <param name="idMedioPago">The idMedioPago.</param>
        /// <returns></returns>
        IBaseDALResponse RegisterPayment(string nroFactura, decimal importe, int idSocio, int idConexion, int idMedioPago);
        /// <summary>
        /// UpdatePayment.
        /// </summary>
        /// <param name="idPlataforma"></param>
        /// <param name="preference"></param>
        /// <param name="TransaccionComercioId"></param>
        /// <returns></returns>
        IBaseDALResponse UpdatePayment(int idPlataforma, string preference, string TransaccionComercioId);
        /// <summary>
        /// Updates Payments.
        /// </summary>
        /// <param name="preference">The idPlataforma.</param>
        /// <param name="estado">The preference.</param>
        /// <param name="collection">The preference.</param>
        /// <param name="merchantOrder">The preference.</param>
        /// <returns></returns>
        IBaseDALResponse UpdatePaymentMP(string preference, Int32 estado, string collection, string merchantOrder);
        /// <summary>
        /// Updates Status Payments.
        /// </summary>
        /// <param name="idPago"></param>
        /// <param name="transaccionPlataformaId"></param>
        /// <param name="estadoPago"></param>
        /// <returns></returns>
        IBaseDALResponse UpdateStatusPayment(int idPago, string transaccionPlataformaId, int estadoPago);
        #endregion
    }
}
