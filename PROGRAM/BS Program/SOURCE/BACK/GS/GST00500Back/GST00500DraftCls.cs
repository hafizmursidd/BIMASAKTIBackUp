using GST00500Common;
using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Common;
using R_CommonFrontBackAPI;

namespace GST00500Back
{
    public class GST00500DraftCls : R_BusinessObject<GST00500DTO>
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
        public List<GST00500DTO> Approval_Draft_List(GST00500DBParameter poEntity)
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
                    $"SELECT VAR_SELECTED = 1, A.*, B.CTRANSACTION_NAME, B.CPROGRAM_ID FROM GST_APPROVAL_O A(NOLOCK) " +
                    $"INNER JOIN GSM_TRANSACTION_CODE B(NOLOCK) ON B.CCOMPANY_ID = A.CCOMPANY_ID " +
                    $"AND B.CTRANSACTION_CODE = A.CTRANSACTION_CODE " +
                    $"WHERE A.CUSER_ID = '{poEntity.CUSER_ID}' AND A.CTRANSACTION_STATUS = '00'";


                loResult = loDb.SqlExecObjectQuery<GST00500DTO>(lcQuery, loConn, true);
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
