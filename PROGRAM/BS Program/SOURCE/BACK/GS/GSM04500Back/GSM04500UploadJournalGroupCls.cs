using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using GSM04500Common;
using System.Transactions;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Back
{
    public class GSM04500UploadJournalGroupCls : R_IBatchProcess
    {

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCommand = loDb.GetCommand();

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04500UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
                var loObject = loTempObject.Select(loTemp => new GSM04500UploadFromExcelDTO
                {
                    JournalGroup = loTemp.JournalGroup,
                    JournalGroupName = loTemp.JournalGroupName,
                    EnableAccrual = loTemp.EnableAccrual
                }).ToList();

                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CJRNGRP_TYPE)).FirstOrDefault().Value;
                var lcJournalGroupType = ((System.Text.Json.JsonElement)loVar2).GetString();

                var loTempVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.COVERWRITE)).FirstOrDefault().Value;
                var lbOverwrite = ((System.Text.Json.JsonElement)loTempVar).GetBoolean();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    lcQuery = $"CREATE TABLE #JRNLGROUP " +
                              $"(No INT, " +
                              $"JournalGroup VARCHAR(8), " +
                              $"JournalGroupName VARCHAR(80), " +
                              $"EnableAccrual BIT," +
                              $"ValidFlag BIT )";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<GSM04500UploadFromExcelDTO>((SqlConnection)loConn, "#JRNLGROUP", loObject);

                    lcQuery = "RSP_GS_UPLOAD_JOURNAL_GROUP";
                    loCommand.CommandText = lcQuery;
                    loCommand.CommandType = CommandType.StoredProcedure;

                    loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);

                    loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                    loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 20, lcJournalGroupType);
                    loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 20, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCommand, "@LOVERWRITE", DbType.Boolean, 20, lbOverwrite);

                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    TransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
