using LMT01500Common.DTO._1._AgreementList;
using LMT01500Common.Logs;
using LMT01500Common.Utilities;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace LMT01500Back
{
    public class LMT01500AgreementListCls
    {
        private readonly LoggerLMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public LMT01500AgreementListCls()
        {
            _loggerLMT01500 = LoggerLMT01500.R_GetInstanceLogger();
            _activitySource = LMT01500Activity.R_GetInstanceActivitySource();
        }

        public List<LMT01500AgreementListOriginalDTO> GetAgreementListDb(LMT01500UtilitiesParameterDTO poParameterInternal, string pcCPROPERTY_ID)
        {
            string? lcMethod = nameof(GetAgreementListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500AgreementListOriginalDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_AGREEMENT_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, pcCPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500AgreementListOriginalDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500AgreementListOriginalDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<LMT01500PropertyListDTO> GetPropertyListDb(LMT01500UtilitiesParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetPropertyListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500PropertyListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);


                _loggerLMT01500.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500PropertyListDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500PropertyListDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public List<LMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCodeDb(string pcCCOMPANY_ID, string pcCTRANS_CODE)
        {
            string? lcMethod = nameof(GetVarGsmTransactionCodeDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500VarGsmTransactionCodeDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set Parameter for Query in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, pcCCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, pcCTRANS_CODE);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500VarGsmTransactionCodeDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500VarGsmTransactionCodeDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public List<LMT01500ComboBoxDTO> GetComboBoxDataCTransStatusDb(LMT01500UtilitiesWithCultureIDParameterDTO poParameterInternal)
        {
            string? lcMethod = nameof(GetComboBoxDataCTransStatusDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500ComboBoxDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                "(@BIMASAKTI, @CCOMPANY_ID, @GS_TRX_STATUS, @NONE, @CULTURE_ID) ";
                _loggerLMT01500.LogDebug("{@ObjectQuery} ", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@BIMASAKTI", DbType.String, 10, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@GS_TRX_STATUS", DbType.String, 15, "_GS_TRX_STATUS");
                loDb.R_AddCommandParameter(loCommand, "@NONE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CULTURE_ID", DbType.String, 8, poParameterInternal.CULTURE_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ComboBoxDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500ComboBoxDTO>(loDataTable).ToList();
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loReturn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public LMT01500ChangeStatusDTO GetChangeStatusDb(LMT01500UtilitiesParameterDTO poParameterInternal, LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetChangeStatusDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loEx = new R_Exception();
            LMT01500ChangeStatusDTO? loRtn = null;
            DataTable? loDataTable;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            R_Db loDb;

            try
            {
                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCmd = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_GET_AGREEMENT_DETAIL";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCmd);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, "802030");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);


                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to data of LMT01500ChangeStatusDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loRtn = R_Utility.R_ConvertTo<LMT01500ChangeStatusDTO>(loDataTable).FirstOrDefault();
#pragma warning disable CS8604 // Possible null reference argument.
                _loggerLMT01500.LogDebug("{@ObjectReturn}", loRtn);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public LMT01500ProcessResultDTO ProcessChangeStatusDb(LMT01500ChangeStatusParameterDTO poParameter)
        {
            string? lcMethod = nameof(ProcessChangeStatusDb);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            using Activity activity = _activitySource.StartActivity(lcMethod);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMT01500ProcessResultDTO? loRtn = null;
            DbConnection? loConn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                loRtn.LRESULT = false;

                _loggerLMT01500.LogInfo(string.Format("initialization R_DB in Method {0}", lcMethod));
                loDb = new();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loDb);

                _loggerLMT01500.LogInfo(string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();
                _loggerLMT01500.LogDebug("{@ObjectDb}", loCommand);


                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Initialize external exceptions For Take Resource Store Procedure in Method {0}", lcMethod));
                R_ExternalException.R_SP_Init_Exception(loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_LM_CHANGE_STATUS_AGREEMENT";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDOC_NO", DbType.String, 50, poParameter.CDOC_NO);
                loDb.R_AddCommandParameter(loCommand, "@CACCEPT_DATE", DbType.String, 50, poParameter.CACCEPT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCHANGE_STATUS_TO", DbType.String, 50, poParameter.CCHANGE_STATUS_TO);
                loDb.R_AddCommandParameter(loCommand, "@CREASON", DbType.String, 50, poParameter.CREASON);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES", DbType.String, 50, poParameter.CNOTES);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    _loggerLMT01500.LogInfo(string.Format("Execute the SQL query for store data to Db in Method {0}", lcMethod));
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                _loggerLMT01500.LogInfo(string.Format("Add external exceptions to loException in Method {0}", lcMethod));
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
                loRtn.LRESULT = !loException.Haserror ? true : false;
            }
            catch (Exception ex)
            {
                loException.Add(ex);

            }

            if (loException.Haserror)
                _loggerLMT01500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMT01500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
