using APT00100COMMON.DTOs.APT00111;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.Loggers;
using APT00100COMMON.DTOs.APT00110;
using R_CommonFrontBackAPI;

namespace APT00100BACK
{
    public class APT00121Cls : R_BusinessObject<APT00121ParameterDTO>
    {
        private LoggerAPT00121 _logger;
        public APT00121Cls()
        {
            _logger = LoggerAPT00121.R_GetInstanceLogger();
        }

        public List<GetProductTypeDTO> GetProductTypeList(GetProductTypeParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetProductTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_GSB_CODE_LIST " +
                    "@CAPPLICATION, " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CCLASS_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_AP_PRODUCT_TYPE");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_GSB_CODE_LIST {@Parameters} || GetProductTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetProductTypeDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public APT00121DTO RefreshInvoiceItem(APT00121RefreshParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            APT00121DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_TRANS_PD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CSEQ_NO, " +
                    "@CREC_ID, " +
                    "@CLOGIN_LANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poParameter.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_TRANS_PD {@Parameters} || RefreshInvoiceItem(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00121DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APT00121ParameterDTO poEntity)
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

                lcQuery = "EXEC RSP_AP_DELETE_TRANS_PD @CREC_ID";

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

        protected override APT00121ParameterDTO R_Display(APT00121ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            APT00121ParameterDTO loResult = new APT00121ParameterDTO();
            APT00121RefreshParameterDTO loParam = null;

            try
            {
                loParam = new APT00121RefreshParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLOGIN_LANGUAGE_ID = poEntity.CLANGUAGE_ID,
                    CREC_ID = poEntity.Data.CREC_ID
                };
                loResult.Data = RefreshInvoiceItem(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Saving(APT00121ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_AP_SAVE_TRANS_PD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CREC_ID, " +
                    "@CACTION, " +
                    "@CREF_NO, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CPROD_DEPT_CODE, " +
                    "@CPROD_TYPE, " +
                    "@CPRODUCT_ID, " +
                    "@CALLOC_ID, " +
                    "@CNOTES, " +
                    "@IBILL_UNIT, " +
                    "@CBILL_UNIT, " +
                    "@NSUPP_CONV_FACTOR, " +
                    "@NTRANS_QTY, " +
                    "@NUNIT_PRICE, " +
                    "@CDISC_TYPE, " +
                    "@NDISC_PCT, " +
                    "@NDISC_AMOUNT, " +
                    "@CTAX_ID, " +
                    "@CTAX_PCT, " +
                    "@NTAX_AMOUNT, " +
                    "@COTHER_TAX_ID, " +
                    "@COTHER_TAX_PCT, " +
                    "@NOTHER_TAX_AMOUNT, " +
                    "@NDIST_DISCOUNT, " +
                    "@NDIST_ADD_ON";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 1, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.Header.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.Header.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "121010");
                loDb.R_AddCommandParameter(loCmd, "@CPROD_DEPT_CODE", DbType.String, 50, poNewEntity.Data.CPROD_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPROD_TYPE", DbType.String, 50, poNewEntity.Data.CPROD_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CPRODUCT_ID", DbType.String, 50, poNewEntity.Data.CPRODUCT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poNewEntity.Data.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNOTES", DbType.String, 255, poNewEntity.Data.CNOTES);
                loDb.R_AddCommandParameter(loCmd, "@IBILL_UNIT", DbType.Int32, 50, poNewEntity.Data.IBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@CBILL_UNIT", DbType.String, 50, poNewEntity.Data.CBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@NSUPP_CONV_FACTOR", DbType.Int32, 50, poNewEntity.Data.NSUPP_CONV_FACTOR);
                loDb.R_AddCommandParameter(loCmd, "@NTRANS_QTY", DbType.Int32, 50, poNewEntity.Data.NTRANS_QTY);
                loDb.R_AddCommandParameter(loCmd, "@NUNIT_PRICE", DbType.Int32, 50, poNewEntity.Data.NUNIT_PRICE);
                loDb.R_AddCommandParameter(loCmd, "@CDISC_TYPE", DbType.String, 50, poNewEntity.Data.CDISC_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_PCT", DbType.Int32, 50, poNewEntity.Data.NDISC_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_AMOUNT", DbType.Int32, 50, poNewEntity.Data.NDISC_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.Data.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PCT", DbType.Int32, 50, poNewEntity.Data.NTAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_AMOUNT", DbType.Int32, 50, poNewEntity.Data.NTAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.Data.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_PCT", DbType.Int32, 50, poNewEntity.Data.NOTHER_TAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_TAX_AMOUNT", DbType.Int32, 50, poNewEntity.Data.NOTHER_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_DISCOUNT", DbType.Int32, 50, poNewEntity.Data.NDIST_DISCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_ADD_ON", DbType.Int32, 50, poNewEntity.Data.NDIST_ADD_ON);

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
