using System;
using System.Data.SqlClient;
using System.Data;
using DALTrylogycWebsite.DALResponses;
using DALTrylogycWebsite.DALResponses.Interfaces;
using DALTrylogycWebsite.Repositories.Interfaces;
using System.Collections.Generic;
using log4net;

namespace DataAccess.Repositories
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public PaymentRepository(ILog log, string connString) : base(log, connString)
        {

        }
        #endregion

        #region Enum
        public enum EstadosPagos
        {
            Creado = 0,
            Aprobado = 1,
            Rechazado = 2,
            Demorado = 3
        }
        #endregion

        #region Method
        /// <summary>
        /// Registers Payments.
        /// </summary>
        /// <param name="nroFactura">The nroFactura.</param>
        /// <param name="importe">The importe.</param>
        /// <param name="idSocio">The idSocio.</param>
        /// <param name="idConexion">The idConexion.</param>
        /// <param name="idMedioPago">The idMedioPago.</param>
        /// <returns></returns>
        public IBaseDALResponse RegisterPayment(string nroFactura, decimal importe, int idSocio, int idConexion, int idMedioPago, List<TrylogycWebsite.Common.DTO.DTOFactura> facturas)
        {
            _log.Info("Register() Comienzo...");
            VerifyConnectionAndCommand();
            
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();
            SqlTransaction sqlTran;
            


            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();
            SqlParameter Prmparametro4 = new SqlParameter();
            SqlParameter Prmparametro5 = new SqlParameter();
            SqlParameter Prmparametro6 = new SqlParameter();

           

            Connection.Open();

            sqlTran = Connection.BeginTransaction();    //Inicio transaccion

            try
            {
                Prmparametro1.ParameterName = "estado";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Bit;
                Prmparametro1.Value = EstadosPagos.Creado;

                Prmparametro2.ParameterName = "numeroFactura";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = "";

                Prmparametro3.ParameterName = "importe";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.Decimal;
                Prmparametro3.Value = importe;

                Prmparametro4.ParameterName = "idSocio";
                Prmparametro4.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro4.Value = idSocio;

                Prmparametro5.ParameterName = "idConexion";
                Prmparametro5.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro5.Value = idConexion;

                Prmparametro6.ParameterName = "idMedioPago";
                Prmparametro6.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro6.Value = idMedioPago;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "INS_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);
                Command.Parameters.Add(Prmparametro4);
                Command.Parameters.Add(Prmparametro5);
                Command.Parameters.Add(Prmparametro6);

             //   Connection.Open();
                
              //   sqlTran = Connection.BeginTransaction();    //Inicio transaccion

                // Command.Transaction = sqlTran;                          //Enlazo transaccion con Cabecera
                

                //int insertedID = Convert.ToInt32(Command.ExecuteScalar());
                dA.SelectCommand = Command;
                dA.SelectCommand.Transaction = sqlTran;
                dA.Fill(response.Results);
                //--GRABO DETALLEDE PAGOS


                var paymentRow = response.Results.Tables[0].Rows[0];

                int idPago = Convert.ToInt32(paymentRow.ItemArray[0]);
                
                foreach (var fact in facturas)
                {
                    SqlCommand commandDetalle = new SqlCommand();
                    commandDetalle.Transaction = sqlTran;

                    SqlParameter PrmParDet1 = new SqlParameter();
                    SqlParameter PrmParDet2 = new SqlParameter();
                    SqlParameter PrmParDet3 = new SqlParameter();
                    SqlParameter PrmParDet4 = new SqlParameter();
                    SqlParameter PrmParDet5 = new SqlParameter();

                    PrmParDet1.ParameterName = "idPago";
                    PrmParDet1.SqlDbType = System.Data.SqlDbType.Int;
                    PrmParDet1.Value =idPago;

                    PrmParDet2.ParameterName = "idSocio";
                    PrmParDet2.SqlDbType = System.Data.SqlDbType.Int;
                    PrmParDet2.Value = fact.IdSocio;

                    PrmParDet3.ParameterName = "idConexion";
                    PrmParDet3.SqlDbType = System.Data.SqlDbType.Int;
                    PrmParDet3.Value = fact.IdConexion;

                    PrmParDet4.ParameterName = "numeroFactura";
                    PrmParDet4.SqlDbType = System.Data.SqlDbType.VarChar;
                    PrmParDet4.Value = fact.NroFactura;
                    
                    PrmParDet5.ParameterName = "importeFactura";
                    PrmParDet5.SqlDbType = System.Data.SqlDbType.Decimal;
                    PrmParDet5.Value = fact.ImporteFactura;

           

                    commandDetalle.CommandType = CommandType.StoredProcedure;
                    commandDetalle.CommandText = "INS_PAGOSDETALLE";
                    commandDetalle.Parameters.Add(PrmParDet1);
                    commandDetalle.Parameters.Add(PrmParDet2);
                    commandDetalle.Parameters.Add(PrmParDet3);
                    commandDetalle.Parameters.Add(PrmParDet4);
                    commandDetalle.Parameters.Add(PrmParDet5);

                    commandDetalle.Connection = Connection;
                    int cantReg= commandDetalle.ExecuteNonQuery();

                }
                
                sqlTran.Commit();
                response.Succeeded = true;
            }

            catch (Exception ex)
            {
                sqlTran.Rollback();
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }
            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"Register() Fin.");
            }

            return response;
        }
        /// <summary>
        /// Updates Payments.
        /// </summary>
        /// <param name="idPlataforma">The idPlataforma.</param>
        /// <param name="preference">The preference.</param>
        /// <returns></returns>
        public IBaseDALResponse UpdatePayment(int idPlataforma, string preference, string TransaccionComercioId)
        {
            _log.Info("Update() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "idPago";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro1.Value = idPlataforma;

                Prmparametro2.ParameterName = "preference";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = preference;

                Prmparametro3.ParameterName = "TransaccionComercioId";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.VarChar;
                if (TransaccionComercioId == null)
                {
                    Prmparametro3.Value = DBNull.Value;
                }
                else {
                    Prmparametro3.Value = TransaccionComercioId;
                }


                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "UPD_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);

                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }
            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"Update() Fin.");
            }

            return response;
        }

        /// <summary>
        /// Updates Payments.
        /// </summary>
        /// <param name="preference">The idPlataforma.</param>
        /// <param name="estado">The preference.</param>
        /// <param name="collection">The preference.</param>
        /// <param name="merchantOrder">The preference.</param>
        /// <returns></returns>
        public IBaseDALResponse UpdatePaymentMP(string preference, Int32 estado, string collection, string merchantOrder)
        {
            _log.Info("UpdatePaymentMP() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();
            SqlParameter Prmparametro4 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "preference";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro1.Value = preference;

                Prmparametro2.ParameterName = "estado";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro2.Value = estado;

                Prmparametro3.ParameterName = "collection";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro3.Value = collection;

                Prmparametro4.ParameterName = "merchantOrder";
                Prmparametro4.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro4.Value = merchantOrder;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "UPD_PAGOS_MP";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);
                Command.Parameters.Add(Prmparametro4);

                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }
            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"Update() Fin.");
            }

            return response;
        }


        /// <summary>
        /// Updates Status Payments.
        /// </summary>
        /// <param name="idPago"></param>
        /// <param name="transaccionPlataformaId"></param>
        /// <param name="estadoPago"></param>
        /// <returns></returns>
        public IBaseDALResponse UpdateStatusPayment(int idPago, string transaccionPlataformaId, int estadoPago)
        {
            _log.Info("Update() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();

            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();

            try
            {
                Prmparametro1.ParameterName = "idPago";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro1.Value = idPago;

                Prmparametro2.ParameterName = "transaccionPlataformaId";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro2.Value = transaccionPlataformaId;

                Prmparametro3.ParameterName = "estadoPago";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro3.Value = estadoPago;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "UPD_STATUS_PAGOS";
                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);

                Connection.Open();
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }

            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }
            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA?.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"Update() Fin.");
            }

            return response;
        }

        public IBaseDALResponse GetPago(int idSocio, int idConexion, string numFact, decimal importe)
        {
            _log.Info("GetPago() Comienzo...");
            VerifyConnectionAndCommand();
            var response = new BaseDALResponse();
            SqlDataAdapter dA = new SqlDataAdapter();
            SqlParameter Prmparametro1 = new SqlParameter();
            SqlParameter Prmparametro2 = new SqlParameter();
            SqlParameter Prmparametro3 = new SqlParameter();
            SqlParameter Prmparametro4 = new SqlParameter();

            try
            {

                Prmparametro1.ParameterName = "idSocio";
                Prmparametro1.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro1.Value = idSocio;


                Prmparametro2.ParameterName = "idConexion";
                Prmparametro2.SqlDbType = System.Data.SqlDbType.Int;
                Prmparametro2.Value = idConexion;

                Prmparametro3.ParameterName = "numFact";
                Prmparametro3.SqlDbType = System.Data.SqlDbType.VarChar;
                Prmparametro3.Value = numFact;


                Prmparametro4.ParameterName = "importe";
                Prmparametro4.SqlDbType = System.Data.SqlDbType.Money;
                Prmparametro4.Value = importe;

                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "SEL_PAGOS";

                Command.Parameters.Add(Prmparametro1);
                Command.Parameters.Add(Prmparametro2);
                Command.Parameters.Add(Prmparametro3);
                Command.Parameters.Add(Prmparametro4);

                Connection.Open();
            
                
                dA.SelectCommand = Command;
                dA.Fill(response.Results);
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Ocurrieron Errores. {ex.Message}");
                response.FillErrorResponse(ex.HResult, ex.Message);
            }


            finally
            {
                _log.Info("Disposing Data Adapter.");
                dA.Dispose();
                Command?.Dispose();
                Command = null;
                _log.Info("Closing connection.");
                Connection?.Close();
                _log.Error($"GetPago() Fin.");
            }

            return response;

        }

        #endregion
    }
    }

