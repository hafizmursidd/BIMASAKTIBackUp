using GSM04500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace GSM04500Back
{
    public class GSM04510GOADeptCls : R_BusinessObject<GSM04510GOADeptDTO>
    {
        protected override void R_Deleting(GSM04510GOADeptDTO poEntity)
        {
            var loEx = new R_Exception();
            DbCommand loCommand;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, poEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CGOA_CODE", DbType.String, 8, poEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CGLACCOUNT_NO", DbType.String, 20, poEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 25, poEntity.CUSER_ID);


                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override GSM04510GOADeptDTO R_Display(GSM04510GOADeptDTO poEntity)
        {
            R_Exception loEexception = new R_Exception();
            GSM04510GOADeptDTO loReturn = null;
            R_Db loDb;

            try
            {
                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_DEPT_DT";

                loDb = new R_Db();
                var loCmd = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 25, poEntity.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM04510GOADeptDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {

                loEexception.Add(ex);
            }
        EndBlock:
            loEexception.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override void R_Saving(GSM04510GOADeptDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                    default:
                        break;
                }

                lcQuery = @"RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_CODE", DbType.String, 8, poNewEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CGOA_CODE", DbType.String, 8, poNewEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 25, poNewEntity.CUSER_ID);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
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
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM04510GOADeptDTO> JOURNAL_GROUP_GOA_DEPT_LIST(GSM04510GOADeptDBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM04510GOADeptDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();
                //EXEC RSP_GS_GET_JOURNAL_GRP_GOA_DEPT_LIST
                //@CCOMPANY_ID, @CPROPERTY_ID, '10' , @CJRNGRP_CODE, @CGOA_CODE, @CUSER_LOGIN_ID
                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_DEPT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poParameter.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poParameter.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poParameter.CGOA_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 25, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM04510GOADeptDTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
}
