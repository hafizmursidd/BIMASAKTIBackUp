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
using System.Windows.Input;

namespace GSM04500Back
{
    public class GSM04510GOACls : R_BusinessObject<GSM04510GOADTO>
    {
        protected override void R_Deleting(GSM04510GOADTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override GSM04510GOADTO R_Display(GSM04510GOADTO poEntity)
        {
            R_Exception loEexception = new R_Exception();
            GSM04510GOADTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_GS_GET_JOURNAL_GRP_GOA_DETAIL";
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
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poEntity.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM04510GOADTO>(loReturnTemp).ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {

                loEexception.Add(ex);
            }
        EndBlock:
            loEexception.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override void R_Saving(GSM04510GOADTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCmd = loDb.GetCommand();

                lcAction = "EDIT";
                lcQuery = @"RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 30, poNewEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poNewEntity.CGOA_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LLDEPARTMENT_MODE", DbType.Boolean, 2, poNewEntity.LDEPARTMENT_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poNewEntity.CUSER_ID);

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

        public List<GSM04510GOADTO> JOURNAL_GROUP_GOA_LIST(GSM04510GOADBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM04510GOADTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();
                //EXEC RSP_GS_GET_JOURNAL_GRP_GOA_LIST
                //@CCOMPANY_ID, @CPROPERTY_ID, CJRNGRP_TYPE ('10') , @CJRNGRP_CODE, @CUSER_LOGIN_ID
                var lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poParameter.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poParameter.CJOURNAL_GRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 25, poParameter.CUSER_ID);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM04510GOADTO>(loReturnTemp).ToList();

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
