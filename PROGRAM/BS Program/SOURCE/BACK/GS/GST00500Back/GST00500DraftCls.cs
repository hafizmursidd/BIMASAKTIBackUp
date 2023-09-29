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
using System.Data;

namespace GST00500Back
{
    public class GST00500DraftCls : R_IServiceCRUDBase<GST00500DTO>
    {
        public R_ServiceGetRecordResultDTO<GST00500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceSaveResultDTO<GST00500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GST00500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public List<GST00500DTO> Approval_Draft_List(GST00500DBParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GST00500DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbCommand loCommand;
            DbConnection loConnection = null;
            string lcQuery;
            try
            {
                loCommand = loDb.GetCommand();
                loConnection = loDb.GetConnection();

                lcQuery = @"RSP_GS_GET_APPR_TRX_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANYID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 2, poEntity.CTRANS_TYPE);

                var loReturnTemp = loDb.SqlExecQuery(loConnection, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GST00500DTO>(loReturnTemp).ToList();
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
