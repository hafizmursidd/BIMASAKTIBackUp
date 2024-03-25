using System.Data;
using System.Data.Common;
using System.Diagnostics;
using LMT01500Common.DTO._4._Charges_Info;
using LMT01500Common.Logs;
using LMT01500Common.Utilities;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMT01500Back
{
    public class LMT01500ChargesInfo_RevenueSharingCls : R_BusinessObject<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>
    {
        private readonly LoggerLMT01500? _loggerLMT01500;
        private readonly ActivitySource _activitySource;

        public LMT01500ChargesInfo_RevenueSharingCls()
        {
            _loggerLMT01500 = LoggerLMT01500.R_GetInstanceLogger();
            _activitySource = LMT01500Activity.R_GetInstanceActivitySource();
        }

        //ITS NOT DONE YET!!!

        public List<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO> GetRevenueSharingSchemeListDb(LMT01500UtilitiesParameterDTO poParameterInternal, LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetRevenueSharingSchemeListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>? loReturn = null;
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
                lcQuery = "RSP_LM_GET_AGREEMENT_CHARGES_REVENUE";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802030");
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(loDataTable).ToList();
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

        public List<LMT01500ChargesInfo_RevenueMinimumRentDTO> GetRevenueMinimumRentListDb(LMT01500UtilitiesParameterDTO poParameterInternal, LMT01500GetHeaderParameterDTO poParameter)
        {
            string? lcMethod = nameof(GetRevenueMinimumRentListDb);
            _loggerLMT01500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<LMT01500ChargesInfo_RevenueMinimumRentDTO>? loReturn = null;
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
                lcQuery = "RSP_LM_GET_AGREEMENT_CHARGES_LIST";
                _loggerLMT01500.LogDebug("{@ObjectTextQuery}", lcQuery);

                _loggerLMT01500.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}", lcMethod));
                DbConnection? loConn = loDb.GetConnection();
                _loggerLMT01500.LogDebug("{@ObjectDbConnection}", loConn);

                _loggerLMT01500.LogInfo(string.Format("Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                _loggerLMT01500.LogDebug("{@ObjectDbCommand}", loCommand);

                _loggerLMT01500.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameterInternal.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802030");
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameterInternal.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLMT01500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _loggerLMT01500.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}", lcMethod));
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _loggerLMT01500.LogInfo(string.Format("Convert the data in loDataTable to a list of LMT01500ChargesInfo_RevenueMinimumRentDTO objects and assign it to loRtn in Method {0}", lcMethod));
                loReturn = R_Utility.R_ConvertTo<LMT01500ChargesInfo_RevenueMinimumRentDTO>(loDataTable).ToList();
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

        protected override LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO R_Display(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(LMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}