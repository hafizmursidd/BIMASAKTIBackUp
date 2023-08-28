using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GST00500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GST00500Back
{
    public class GST00500Cls : R_BusinessObject<GST00500DTO>
    {
        protected override GST00500DTO R_Display(GST00500DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(GST00500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(GST00500DTO poEntity)
        {
            throw new NotImplementedException();
        }

        public List<GST00500DTO> Approval_Inbox_List(GST00500DBParameter poEntity)
        {
            var loEx = new R_Exception();
            var loResult = new List<GST00500DTO>();
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCommand = loDb.GetCommand();

                var lcQuery = $"SELECT VAR_SELECTED = 0, A.*, C.CDESCRIPTION AS CAPPROVAL_STATUS_DESC, B.CTRANSACTION_NAME, " +
                              $"B.CTABLE_NAME, B.CPROGRAM_ID FROM GST_APPROVAL_I A (NOLOCK) " +
                              $"INNER JOIN GSM_TRANSACTION_CODE B (NOLOCK) " +
                              $"ON B.CCOMPANY_ID = A.CCOMPANY_ID AND B.CTRANSACTION_CODE = A.CTRANSACTION_CODE " +
                              $"INNER JOIN RFT_GET_GSB_CODE_INFO('SIAPP', '{poEntity.CCOMPANYID}', " +
                              $"'_GS_APPROVAL_STATUS', '', '{poEntity.CLANGUAGE_ID}') C " +
                              $"ON C.CCODE = A.CAPPROVAL_STATUS " +
                              $"WHERE A.CUSER_ID = '{poEntity.CUSER_ID}' AND A.CAPPROVAL_STATUS = '01'  AND A.CTRANSACTION_STATUS IN ('01','02') " +
                              $"ORDER BY B.CTRANSACTION_NAME, A.CREFERENCE_DATE ASC";

                loResult = loDb.SqlExecObjectQuery<GST00500DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        
        public GST00500UserNameDTO GetUserName(GST00500DBParameter poEntity)
        {
            var loEx = new R_Exception();
            var loResult = new GST00500UserNameDTO();
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCommand = loDb.GetCommand();

                var lcQuery = $"SELECT B.CUSER_NAME FROM SAM_USER_COMPANY A (NOLOCK) " +
                              $"INNER JOIN SAM_USER B (NOLOCK) ON B.CUSER_ID = A.CUSER_ID " +
                              $"WHERE A.CCOMPANY_ID = '{poEntity.CCOMPANYID}' AND A.CUSER_ID = '{poEntity.CUSER_ID}' ";
                var loResultTemp = loDb.SqlExecQuery(lcQuery, loConn, true);
                loResult.CUSER_NAME = loResultTemp.Rows[0]["CUSER_NAME"].ToString();

                //   loResult = loDb.SqlExecObjectQuery<GST00500UserNameDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<GST00500RejectDTO> GetReasonRejectList(GST00500DBParameter poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new List<GST00500RejectDTO>();
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCommand = loDb.GetCommand();

                var lcQuery = $"SELECT * FROM RFT_GET_GSB_CODE_INFO('SIAPP', " +
                              $"'{poParameter.CCOMPANYID}', '_GS_REJECTTRANS_REASON', '', " +
                              $"'{poParameter.CLANGUAGE_ID}') ";

                loResult = loDb.SqlExecObjectQuery<GST00500RejectDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public List<GST00500ApprovalTransactionDTO> GetErrorList(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<GST00500ApprovalTransactionDTO> loResult = null;
            DbConnection loConn = null;

            try
            {
                loResult = new List<GST00500ApprovalTransactionDTO>();
                loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<GST00500ApprovalTransactionDTO>(loDataTableResult).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        

    }
}
