using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.Loggers;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;

namespace APT00100BACK
{
    public class APT00110Cls : R_BusinessObject<APT00110ParameterDTO>
    {
        private LoggerAPT00110 _logger;
        public APT00110Cls()
        {
            _logger = LoggerAPT00110.R_GetInstanceLogger();
        }

        public List<GetPaymentTermListDTO> GetPaymentTermList(GetPaymentTermListParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetPaymentTermListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_PAYMENT_TERM_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PAYMENT_TERM_LIST {@Parameters} || GetPaymentTermList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPaymentTermListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<GetCurrencyListDTO> GetCurrencyList(GetCurrencyListParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetCurrencyListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_CURRENCY_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CURRENCY_LIST {@Parameters} || GetCurrencyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrencyListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public GetCurrencyOrTaxRateDTO GetCurrencyOrTaxRate (GetCurrencyOrTaxRateParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            GetCurrencyOrTaxRateDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_LAST_CURRENCY_RATE " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CCURRENCY_CODE, " +
                    "@CRATETYPE_CODE, " +
                    "@CREF_DATE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParam.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poParam.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poParam.CREF_DATE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_LAST_CURRENCY_RATE {@Parameters} || GetCurrencyRate(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrencyOrTaxRateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public APT00110DTO GetInvoiceHeader(GetInvoiceHeaderParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            APT00110DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_HD @CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_HD {@Parameters} || GetInvoiceHeader(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00110DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APT00110ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_DELETE_TRANS_HD @CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_DELETE_TRANS_HD {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override APT00110ParameterDTO R_Display(APT00110ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            APT00110ParameterDTO loResult = new APT00110ParameterDTO();
            GetInvoiceHeaderParameterDTO loParam = null;

            try
            {
                loParam = new GetInvoiceHeaderParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLANGUAGE_ID = poEntity.CLANGUAGE_ID,
                    CREC_ID = poEntity.Data.CREC_ID
                };
                loResult.Data = GetInvoiceHeader(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Saving(APT00110ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_AP_SAVE_TRANS_HD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CACTION, " +
                    "@CREC_ID, " +
                    "@CREF_NO, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_DATE, " +
                    "@CSUPPLIER_ID, " +
                    "@CSUPPLIER_SEQ_NO, " +
                    "@CSUPPLIER_NAME, " +
                    "@LONETIME, " +
                    "@CDOC_NO, " +
                    "@CDOC_DATE, " +
                    "@CTERM_CODE, " +
                    "@CDUE_DATE, " +
                    "@CTRANS_DESC, " +
                    "@CCURRENCY_CODE, " +
                    "@NLCURRENCY_BASE_RATE, " +
                    "@NLCURRENCY_RATE, " +
                    "@NBCURRENCY_BASE_RATE, " +
                    "@NBCURRENCY_RATE, " +
                    "@LTAXABLE, " +
                    "@CTAX_ID, " +
                    "@NTAX_PERCENTAGE, " +
                    "@NTAX_BASE_RATE, " +
                    "@NTAX_RATE, " +
                    "@CSOURCE_MODULE, " +
                    "@CTRANS_STATUS";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.Boolean, 1, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "110010");
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poNewEntity.Data.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 50, poNewEntity.Data.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_SEQ_NO", DbType.String, 50, poNewEntity.Data.CSUPPLIER_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 50, poNewEntity.Data.CSUPPLIER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LONETIME", DbType.Boolean, 50, poNewEntity.Data.LONETIME);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.Data.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 50, poNewEntity.Data.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTERM_CODE", DbType.String, 50, poNewEntity.Data.CPAY_TERM_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDUE_DATE", DbType.String, 50, poNewEntity.Data.CDUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 50, poNewEntity.Data.CTRANS_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_BASE_RATE", DbType.Int32, 50, poNewEntity.Data.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Int32, 50, poNewEntity.Data.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_BASE_RATE", DbType.Int32, 50, poNewEntity.Data.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Int32, 50, poNewEntity.Data.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poNewEntity.Data.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.Data.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.String, 50, poNewEntity.Data.NTAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_BASE_RATE", DbType.String, 50, poNewEntity.Data.NTAX_BASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_RATE", DbType.String, 50, poNewEntity.Data.NTAX_CURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, 50, "AP");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 50, "00");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_SAVE_TRANS_HD {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
