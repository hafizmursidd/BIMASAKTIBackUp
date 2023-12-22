using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_CommonFrontBackAPI;
using R_Common;
using System.Data.Common;
using System.Data;

namespace LMM01500BACK
{
    public class LMM01500PenaltyCls : R_BusinessObject<LMM01500PenaltyDTO>
    {
        protected override LMM01500PenaltyDTO R_Display(LMM01500PenaltyDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500PenaltyDTO loReturn = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                var lcQuery = "RSP_LM_GET_INVOICE_GROUP";
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loReturn = R_Utility.R_ConvertTo<LMM01500PenaltyDTO>(loDataTable).FirstOrDefault();
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

            return loReturn;
        }

        protected override void R_Saving(LMM01500PenaltyDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                lcQuery = "RSP_LM_MAINTAIN_INVGRP_PENALTY";
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LPENALTY", DbType.Boolean, 50, poNewEntity.LPENALTY);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_ADD_ID", DbType.String, 50, poNewEntity.CPENALTY_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE", DbType.String, 50, poNewEntity.CPENALTY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NPENALTY_TYPE_VALUE", DbType.Int32, 50, poNewEntity.NPENALTY_TYPE_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_TYPE_CALC_BASEON", DbType.String, 50, poNewEntity.CPENALTY_TYPE_CALC_BASEON);
                loDb.R_AddCommandParameter(loCmd, "@IROUNDED", DbType.Int32, 50, poNewEntity.IROUNDED);
                loDb.R_AddCommandParameter(loCmd, "@CCUTOFDATE_BY", DbType.String, 50, poNewEntity.CCUTOFDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@IGRACE_PERIOD", DbType.Int32, 50, poNewEntity.IGRACE_PERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CPENALTY_FEE_START_FROM", DbType.String, 50, poNewEntity.CPENALTY_FEE_START_FROM);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_HOLIDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SATURDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LEXCLUDE_SPECIAL_DAY_SUNDAY", DbType.Boolean, 50, poNewEntity.LEXCLUDE_SPECIAL_DAY_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@NMIN_PENALTY_AMOUNT", DbType.String, 50, poNewEntity.NMIN_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NMAX_PENALTY_AMOUNT", DbType.String, 50, poNewEntity.NMAX_PENALTY_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

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
                loEx.Add(ex); }
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

        protected override void R_Deleting(LMM01500PenaltyDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
