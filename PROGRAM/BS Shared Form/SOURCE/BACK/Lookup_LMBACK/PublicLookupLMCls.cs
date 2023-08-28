using System.Data;
using System.Runtime.InteropServices;
using Lookup_LMCOMMON.DTOs;
using R_BackEnd;
using R_Common;

namespace Lookup_LMBACK
{
    public class PublicLookupLMCls
    {
        public List<LML00100DTO> GetAllSalesTax(LML00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LML00100DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loCmd = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                var lcQuery = $@"RSP_GS_GET_SALES_TAX_LIST '{poEntity.CCOMPANY_ID}', '{poEntity.CUSER_ID}' ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loResult = loDb.SqlExecObjectQuery<LML00100DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<LML00200DTO> GetAllUnitCharges(LML00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LML00200DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_CHARGES_TYPE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 25, poEntity.CCHARGE_TYPE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00200DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LML00300DTO> GetAllSupervisor(LML00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LML00300DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_SUPERVISOR_LOOKUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<LML00400DTO> GetAllUtilityCharges(LML00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LML00400DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_CHARGES_UTILITY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 99, poEntity.CCHARGE_TYPE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00400DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}