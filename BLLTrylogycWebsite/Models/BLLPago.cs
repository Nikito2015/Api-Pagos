using BLLTrylogycWebsite.Enums;
using BLLTrylogycWebsite.Models.Interfaces;
using BLLTrylogycWebsite.Responses;
using BLLTrylogycWebsite.Responses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using CommonTrylogycWebsite.DTO;
using DataAccess.Repositories;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml;

namespace BLLTrylogycWebsite.Models
{
    public class BLLPago : BLLBase, IBLLPago
    {
        #region Private Members
        private IPaymentRepository _paymentRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BLLPago"/> class.
        /// </summary>
        /// <param name="log"></param>
        public BLLPago(ILog log, string connString, IConfiguration configuration) : base(log, connString, configuration)
        {
            _paymentRepository = new PaymentRepository(log, connString);
        }

        #endregion
        /// <summary>
        /// RegisterPayment.
        /// </summary>
        /// <param name="nroFactura"></param>
        /// <param name="importe"></param>
        /// <param name="idSocio"></param>
        /// <param name="idConexion"></param>
        /// <param name="idMedioPago"></param>
        /// <param name="facturas"></param>
        /// <returns></returns>
        public IBLLResponseBase<int> RegisterPayment(string nroFactura, decimal importe, int idSocio, int idConexion, int idMedioPago,List<TrylogycWebsite.Common.DTO.DTOFactura>  facturas )
        {
            _log.Info("RegisterPayment() Comienzo...");
            var bllRegisterPaymentResponse = new BLLResponseBase<int>();

            try
            {
                _log.Info($"Recuperando nroFactura {nroFactura}.");              
                var RegisterPaymentDalResponse = _paymentRepository.RegisterPayment(nroFactura, importe, idSocio, idConexion, idMedioPago,facturas );
                if (RegisterPaymentDalResponse.Succeeded)
                {
                    if(RegisterPaymentDalResponse.Results.Tables?[0]?.Rows?.Count > 0)
                    {
                        _log.Info("Convirtiendo dataset a DTO.");
                        var paymentRow = RegisterPaymentDalResponse.Results.Tables[0].Rows[0];

                        bllRegisterPaymentResponse.DTOResult = Convert.ToInt32(paymentRow.ItemArray[0]);

                        bllRegisterPaymentResponse.Status = Status.Success;
                        bllRegisterPaymentResponse.Message = "Se registró el pago correctamente.";
                    }
                }
                else
                {
                    _log.Info($"Ocurrieron errores al registar el pago {nroFactura}.");
                    bllRegisterPaymentResponse.Status = Enums.Status.Fail;
                    bllRegisterPaymentResponse.Message = "Ocurrieron Errores al registrar el pago.";
                }
    
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar al registrar el pago {nroFactura}. {ex.Message}");
                bllRegisterPaymentResponse.Status = Enums.Status.Fail;
                bllRegisterPaymentResponse.Message = $"Ocurrieron errores al intentar al registrar el pago.";
            }

            return bllRegisterPaymentResponse;
        }

        /// <summary>
        /// UpdatePayment.
        /// </summary>
        /// <param name="idPlataforma"></param>
        /// <param name="preference"></param>
        /// <returns></returns>
        public IBLLResponseBase<bool> UpdatePayment(int idPlataforma,  string preference, string TransaccionComercioId)
        {
            _log.Info("UpdatePayment() Comienzo...");
            var bllUpdatePaymentResponse = new BLLResponseBase<bool>();

            try
            {
                _log.Info($"Recuperando preference {preference}.");              
                var UpdatePaymenttDalResponse = _paymentRepository.UpdatePayment(idPlataforma, preference, TransaccionComercioId);
                if (UpdatePaymenttDalResponse.Succeeded)
                {
                    bllUpdatePaymentResponse.Status = Status.Success;
                    bllUpdatePaymentResponse.Message = "Se actualizó el pago correctamente.";
                }
                else
                {
                    _log.Info($"Ocurrieron errores al actualizó el pago {preference}.");
                    bllUpdatePaymentResponse.Status = Enums.Status.Fail;
                    bllUpdatePaymentResponse.Message = "Ocurrieron Errores al actualizó el pago.";
                }
    
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar al actualizó el pago {preference}. {ex.Message}");
                bllUpdatePaymentResponse.Status = Enums.Status.Fail;
                bllUpdatePaymentResponse.Message = $"Ocurrieron errores al intentar al actualizó el pago.";
            }

            return bllUpdatePaymentResponse;
        }

        /// <summary>
        /// UpdatePaymentMP.
        /// </summary>
        /// <param name="preference"></param>
        /// <param name="estado"></param>
        /// <param name="collection"></param>
        /// <param name="merchantOrder"></param>
        /// <returns></returns>
        public IBLLResponseBase<bool> UpdatePaymentMP(string preference,int estado,string collection, string merchantOrder)
        {
            _log.Info("UpdatePaymentMP() Comienzo...");
            var bllUpdatePaymentResponse = new BLLResponseBase<bool>();

            try
            {
                _log.Info($"Recuperando preference {preference}.");
                var UpdatePaymenttDalResponse = _paymentRepository.UpdatePaymentMP(preference, estado, collection, merchantOrder);
                if (UpdatePaymenttDalResponse.Succeeded)
                {
                    bllUpdatePaymentResponse.Status = Status.Success;
                    bllUpdatePaymentResponse.Message = "Se actualizó el pago correctamente.";
                }
                else
                {
                    _log.Info($"Ocurrieron errores al actualizó el pago {preference}.");
                    bllUpdatePaymentResponse.Status = Enums.Status.Fail;
                    bllUpdatePaymentResponse.Message = "Ocurrieron Errores al actualizó el pago.";
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar al actualizó el pago {preference}. {ex.Message}");
                bllUpdatePaymentResponse.Status = Enums.Status.Fail;
                bllUpdatePaymentResponse.Message = $"Ocurrieron errores al intentar al actualizó el pago.";
            }

            return bllUpdatePaymentResponse;
        }

        /// <summary>
        /// UpdateStatusPayment.
        /// </summary>
        /// <param name="preference"></param>
        /// <param name="transaccionPlataformaId"></param>
        /// <param name="estadoPago"></param>
        /// <returns></returns>
        public IBLLResponseBase<bool> UpdateStatusPayment(int idPago, string transaccionPlataformaId, int estadoPago)
        {
            _log.Info("UpdateStatusPayment() Comienzo...");
            var bllUpdateStatusPaymentResponse = new BLLResponseBase<bool>();

            try
            {
                _log.Info($"Recuperando preference {idPago}.");
                var UpdateStatusPaymenttDalResponse = _paymentRepository.UpdateStatusPayment(idPago, transaccionPlataformaId, estadoPago);
                if (UpdateStatusPaymenttDalResponse.Succeeded)
                {
                    bllUpdateStatusPaymentResponse.Status = Status.Success;
                    bllUpdateStatusPaymentResponse.Message = "Se actualizó el pago correctamente.";
                }
                else
                {
                    _log.Info($"Ocurrieron errores al actualizó el pago {idPago}.");
                    bllUpdateStatusPaymentResponse.Status = Enums.Status.Fail;
                    bllUpdateStatusPaymentResponse.Message = "Ocurrieron Errores al actualizó el pago.";
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar al actualizó el pago {idPago}. {ex.Message}");
                bllUpdateStatusPaymentResponse.Status = Enums.Status.Fail;
                bllUpdateStatusPaymentResponse.Message = $"Ocurrieron errores al intentar al actualizó el pago.";
            }

            return bllUpdateStatusPaymentResponse;
        }

        /// <summary>
        /// GetPago.
        /// </summary>
        /// <param name="idSocio"></param>
        /// <param name="idConexion"></param>
        /// <param name="numfact"></param>
        /// <param name="Importe"></param>
        /// <returns></returns>
        public IBLLResponseBase<bool> GetPago(int idSocio, int idConexion, string numfact, decimal Importe)
        {
            _log.Info("GetPago() Comienzo...");
            var bllPagoResponse = new BLLResponseBase<bool>();

            try
            {
                _log.Info($"Recuperando pago {idSocio}.");
                var pagoDalResponse = _paymentRepository.GetPago(idSocio,idConexion,numfact,Importe);
                if (pagoDalResponse.Succeeded)
                {
                    // pagoDalResponse.Results.Tables[0].Rows[0].Field[0]

                    //var paymentRow = pagoDalResponse.Results
                    bllPagoResponse.DTOResult = (pagoDalResponse.Results.Tables[0].Rows.Count > 0 ? true : false);
                    bllPagoResponse.Status = Status.Success;
                    bllPagoResponse.Message = (pagoDalResponse.Results.Tables[0].Rows.Count > 0 ? "Existen pagos pendientes para la factura consultada." : "NO existen pagos pendientes para la factura consultada."); 
                }
                else
                {
                    _log.Info($"Ocurrieron errores al consultar pagos para: {idSocio}.");
                    bllPagoResponse.Status = Enums.Status.Fail;
                    bllPagoResponse.Message = "Ocurrieron errores al consultar pagos.";
                }

            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron errores al intentar consultar pagos para el socio {idSocio}. {ex.Message}");
                bllPagoResponse.Status = Enums.Status.Fail;
                bllPagoResponse.Message = $"Ocurrieron errores al intentar al actualizó el pago.";
            }

            return bllPagoResponse;
        }
    }
}
