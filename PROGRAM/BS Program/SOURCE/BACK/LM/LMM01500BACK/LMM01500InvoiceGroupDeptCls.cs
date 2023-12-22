using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_StorageCommon;

namespace LMM01500BACK
{
    public class LMM01500InvoiceGroupDeptCls : R_BusinessObject<LMM01500InvoiceGrpDeptDetailDTO>
    {
        public List<LMM01500InvoiceGrpDeptDTO> GetInvoiceGroupDeptList(LMM01500DBParam poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM01500InvoiceGrpDeptDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_INVGRP_DEPT_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 50, poParameter.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 25, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM01500InvoiceGrpDeptDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override LMM01500InvoiceGrpDeptDetailDTO R_Display(LMM01500InvoiceGrpDeptDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM01500InvoiceGrpDeptDetailDTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_DETAIL";
                loDb = new R_Db();
                var loCommand = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loReturn = R_Utility.R_ConvertTo<LMM01500InvoiceGrpDeptDetailDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(LMM01500InvoiceGrpDeptDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand = null;
            DbConnection loConn = null;
            string lcAction = null;
            R_SaveResult loSaveResult;
            loDb = new R_Db();
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                loConnAttr = loDb.GetConnectionAttribute();

                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    //Set Action 
                    lcAction = "ADD";

                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                }

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 8, poNewEntity.CINVGRP_CODE); ;
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 8, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_TEMPLATE", DbType.String, 100, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_CODE", DbType.String, 8, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_ACCOUNT", DbType.String, 20, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

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

        private void SaveDataSP(LMM01500InvoiceGrpDeptDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            LMM01500InvoiceGrpDeptDetailDTO loResult = null;
            string lcAction;

            try
            {
                //Set Action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                   lcAction = "EDIT";
                }

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
/*
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poNewEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poNewEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poNewEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poNewEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poNewEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poNewEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
*/
                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(LMM01500InvoiceGrpDeptDetailDTO poEntity)
        {
            var loException = new R_Exception();
            DbCommand loCommand;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE); ;
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
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
            loException.ThrowExceptionIfErrors();
        }
    }
}
