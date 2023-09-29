using GLR00300Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using GLR00300Common.GLR00300Print;
using System.Reflection.Metadata;

namespace GLR00300Back
{
    public class GLR00300Cls
    { public GLR00300PeriodDTO IntialProcess(GLR00300DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            GLR00300PeriodDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE ";

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, "");
                loDb.R_AddCommandParameter(loCommand, "@CMODE", DbType.String, 10, "");

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<GLR00300PeriodDTO>(loReturnTemp).FirstOrDefault();

                //Initial Process GET DEFAULT YEAR
                var lcQueryDefaultYear = $"EXEC RSP_GL_GET_SYSTEM_PARAM '{poParameter.CCOMPANY_ID}', '{poParameter.CLANGUAGE_ID}' ";
                loCommand.CommandText = lcQueryDefaultYear;
                loCommand.CommandType = CommandType.Text;
                var loReturnTempVal = loDb.SqlExecQuery(loConn, loCommand, true);
                var loResultTemp = R_Utility.R_ConvertTo<GLR00300PeriodDTO>(loReturnTempVal).FirstOrDefault();

                loResult.CSOFT_PERIOD_YY = loResultTemp.CSOFT_PERIOD_YY;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public List<GLR00300DTO> GetTrialBalanceTypeList(GLR00300DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLR00300DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI"); 
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, "_GL_TRIAL_BAL_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public List<GLR00300DTO> GetPrintMethodTypeList(GLR00300DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLR00300DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, "_GL_TRIAL_BAL_PRINT_METHOD");
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public List<GLR00300BudgetNoDTO> GetBudgetNoList(GLR00300DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLR00300BudgetNoDTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_GET_BUDGET_FOR_TB_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_TYPE", DbType.String, 10, poParameter.CCURRENCY_TYPE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300BudgetNoDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public List<GLR00300_DataDetail_AccountTrialBalance> GetAllTrialBalanceReportData(GLR00300ParamDBToGetReportDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLR00300_DataDetail_AccountTrialBalance> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_REP_TRIAL_BALANCE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 6, poParameter.CLANGUAGE_ID);

                loDb.R_AddCommandParameter(loCommand, "@CTB_TYPE", DbType.String, 2, poParameter.CTB_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_ADJ_MODE", DbType.String, 2, poParameter.CJOURNAL_ADJ_MODE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_TYPE", DbType.String, 2, poParameter.CCURRENCY_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_ACCOUNT_NO", DbType.String, 20, poParameter.CFROM_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "@CTO_ACCOUNT_NO", DbType.String, 20, poParameter.CTO_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_CENTER_CODE", DbType.String, 20, poParameter.CFROM_CENTER_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTO_CENTER_CODE", DbType.String, 20, poParameter.CTO_CENTER_CODE);

                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_PERIOD_NO", DbType.String, 2, poParameter.CFROM_PERIOD_NO);
                loDb.R_AddCommandParameter(loCommand, "@CTO_PERIOD_NO", DbType.String, 2, poParameter.CTO_PERIOD_NO);
                loDb.R_AddCommandParameter(loCommand, "@CPRINT_METHOD", DbType.String, 10, poParameter.CPRINT_METHOD_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUDGET_NO", DbType.String, 20, poParameter.CBUDGET_NO);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300_DataDetail_AccountTrialBalance>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }



    }
}