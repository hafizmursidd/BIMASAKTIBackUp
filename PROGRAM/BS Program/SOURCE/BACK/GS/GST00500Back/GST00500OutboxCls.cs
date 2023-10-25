﻿using GST00500Common;
using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using R_Common;
using System.Data.Common;
using System.Data;
using GST00500Common.Logs;

namespace GST00500Back
{
    public class GST00500OutboxCls : R_IServiceCRUDBase<GST00500DTO>
    {
        private LoggerGST00500 _loggerGST00500;
        public GST00500OutboxCls()
        {
            //Initial and Get Logger
            _loggerGST00500 = LoggerGST00500.R_GetInstanceLogger();
        }

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
        public List<GST00500DTO> Approval_Outbox_List(GST00500DBParameter poEntity)
        {
            string lcMethodName = nameof(Approval_Outbox_List);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            var loException = new R_Exception();
            List<GST00500DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbCommand loCommand;
            DbConnection loConnection = null;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConnection = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_APPR_TRX_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANYID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 2, poEntity.CTRANS_TYPE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.R_LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConnection, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GST00500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGST00500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;

        }

        public List<GST00500ApprovalStatusDTO> ApproverStatusList(GST00500DBParameter poParameter)
        {
            string lcMethodName = nameof(ApproverStatusList);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loException = new R_Exception();
            List<GST00500ApprovalStatusDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbCommand loCommand;
            DbConnection loConnection = null;
            string lcQuery;
            try
            {
                loConnection = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_APPROVER_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANYID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_LOGIN_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.R_LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loResultTemp = loDb.SqlExecQuery(loConnection, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GST00500ApprovalStatusDTO>(loResultTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGST00500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }


    }
}


