using GST00500Common;
using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using R_Common;
using System.Data.Common;

namespace GST00500Back
{
    public class GST00500OutboxCls : R_BusinessObject<GST00500DTO>
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
        public List<GST00500DTO> Approval_Outbox_List(GST00500DBParameter poEntity)
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

                var lcQuery =
                    $"SELECT VAR_SELECTED = 1, A.*, C.CDESCRIPTION AS CTRANSACTION_STATUS_DESC, B.CTRANSACTION_NAME, " +
                    $"B.CPROGRAM_ID FROM GST_APPROVAL_O A(NOLOCK) " +
                    $"INNER JOIN GSM_TRANSACTION_CODE B(NOLOCK) ON B.CCOMPANY_ID = A.CCOMPANY_ID " +
                    $"AND B.CTRANSACTION_CODE = A.CTRANSACTION_CODE " +
                    $"INNER JOIN DBO.RFT_GET_GSB_CODE_INFO('SIAPP', '{poEntity.CCOMPANYID}', " +
                    $"'_STATUS_FLAG_01', '', '{poEntity.CLANGUAGE_ID}') C " +
                    $"ON C.CCODE = A.CTRANSACTION_STATUS " +
                    $"WHERE A.CUSER_ID = '{poEntity.CUSER_ID}' AND A.CTRANSACTION_STATUS IN('01','02') " +
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
        public List<GST00500ApprovalStatusDTO> ApprovalStatus(GST00500DTO poEntity, GST00500DBParameter poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new List<GST00500ApprovalStatusDTO>();
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                var loCommand = loDb.GetCommand();

                var lcQuery =
                    $"SELECT C.*, D.CUSER_NAME, D.CPOSITION, E.CDESCRIPTION AS CAPPROVAL_STATUS_DESC FROM GST_APPROVAL_I C (NOLOCK) " +
                    $"INNER JOIN SAM_USER D (NOLOCK) ON D.CUSER_ID = C.CUSER_ID " +
                    $"INNER JOIN RFT_GET_GSB_CODE_INFO ('SIAPP', '{poParameter.CCOMPANYID}', '_GS_APPROVAL_STATUS', '', '{poParameter.CLANGUAGE_ID}') E " +
                    $"ON C.CAPPROVAL_STATUS = E.CCODE " +
                    $"WHERE C.CCOMPANY_ID     = '{poParameter.CCOMPANYID}' " +
                    $"AND C.CTRANSACTION_CODE = '{poEntity.CTRANSACTION_CODE}' " +
                    $"AND C.CDEPT_CODE        = '{poEntity.CDEPT_CODE}' " +
                    $"AND C.CREFERENCE_NO     = '{poEntity.CREFERENCE_NO}' ORDER BY C.CUSER_ID, C.ISEQUENCE ASC ";

                var loResultTemp = loDb.SqlExecQuery(lcQuery, loConn, true);
                loResult = R_Utility.R_ConvertTo<GST00500ApprovalStatusDTO>(loResultTemp).ToList();
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

