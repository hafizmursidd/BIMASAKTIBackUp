using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Transactions;
using GLB00200Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLB00200Back
{
    public class GLB00200Cls
    {
        public List<GLB00200DTO> ReversingJournalProcessList(GLB00200DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLB00200DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_SEARCH_ACTIVE_REVERSING_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 6, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CSEARCH_TEXT", DbType.String, 30, poParameter.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLB00200DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public GLB00200InitalProcessDTO InitialProcess(GLB00200DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            GLB00200InitalProcessDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                //for GET YEAR MIN MAX __ VALIDATION
                var lcQuery =
                    @"SELECT IMIN_YEAR = CAST(MIN(CYEAR) AS INT),IMAX_YEAR = CAST(MAX(CYEAR) AS INT) FROM GSM_PERIOD(NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<GLB00200InitalProcessDTO>(loReturnTemp).FirstOrDefault();

                //for LINCREMENT __ VALIDATION
                var lcQueryLincrement = $"SELECT LINCREMENT_FLAG,LAPPROVAL_FLAG FROM GSM_TRANSACTION_CODE (NOLOCK) " +
                                        $"WHERE CCOMPANY_ID = @CCOMPANY_ID AND CTRANSACTION_CODE ='000030' ";
                loCommand.CommandText = lcQueryLincrement;
                loCommand.CommandType = CommandType.Text;
                //   loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                var loReturnTempVal = loDb.SqlExecQuery(loConn, loCommand, true);
                var loResultTemp = R_Utility.R_ConvertTo<GLB00200InitalProcessDTO>(loReturnTempVal).FirstOrDefault();

                loResult.LINCREMENT_FLAG = loResultTemp.LINCREMENT_FLAG;
                loResult.LAPPROVAL_FLAG = loResultTemp.LAPPROVAL_FLAG;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<GLB00200JournalDetailDTO> GetDetail_ReversingJournalList(GLB00200DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GLB00200JournalDetailDTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_GET_JOURNAL_DETAIL_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CJRN_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLB00200JournalDetailDTO>(loReturnTemp).ToList();
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