using R_BackEnd;
using R_Common;
using System.Data.Common;
using GSM04500Common;
using System.Data;
using System.Data.SqlClient;
using R_CommonFrontBackAPI;
using System.Transactions;
using System.Text.Json;
using System.Windows.Input;

namespace GSM04500Back
{
    public class GSM04500ValidateUploadTemplateCls :R_IBatchProcess
    {
        public GSM04500ListDTO GetUploadJournalGroupList(GSM04500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            GSM04500ListDTO loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCommand = loDb.GetCommand();
                loResult = new GSM04500ListDTO();

                string lcQuery =
                    $"SELECT * FROM GSM_JRNGRP (NOLOCK) WHERE CCOMPANY_ID =@CCOMPANY_ID AND CPROPERTY_ID = @CPROPERTY_ID " +
                    "AND CJRNGRP_TYPE =@CJRNGRP_TYPE ";
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 25, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CJRNGRP_TYPE", DbType.String, 5, poParameter.CJRNGRP_TYPE);

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                var lotemp = R_Utility.R_ConvertTo<GSM04500DTO>(loDataTable).ToList();
                loResult.ListData = lotemp;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcCmd;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCommand = loDb.GetCommand();
            List<GSM04500UploadToDBDTO> loResult = null;
            int count = 1;
            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04500UploadToDBDTO>>(poBatchProcessPar.BigObject);

                //convert to aother DTO
                List<GSM04500UploadFromExcelDTO> loObjectFromExcel = new List<GSM04500UploadFromExcelDTO>();

                foreach (var item in loTempObject)
                {

                    loObjectFromExcel.Add(new GSM04500UploadFromExcelDTO()
                    {
                        No = count,
                        JournalGroup = item.JournalGroup,
                        JournalGroupName = item.JournalGroupName,
                        EnableAccrual = item.EnableAccrual
                    });
                    count++;
                };

                //get parameter from front
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CJRNGRP_TYPE)).FirstOrDefault().Value;
                var loVar3 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.COVERWRITE)).FirstOrDefault().Value;

                var lcPropertyId = ((JsonElement)loVar).GetString();
                var lcJournalGroupType = ((JsonElement)loVar2).GetString();
                bool loIsOverwrite = ((JsonElement)loVar3).GetBoolean();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    //create table temporary
                    lcQuery = $"CREATE TABLE #JRNLGROUP " +
                              $"(No INT, " +
                              $"JournalGroup VARCHAR(8), " +
                              $"JournalGroupName VARCHAR(80), " +
                              $"EnableAccrual BIT," +
                              $"ValidFlag BIT )";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert((SqlConnection)loConn, "#JRNLGROUP", loObjectFromExcel);

                    //validate data
                    lcQuery = "RSP_GS_VALIDATE_UPLOAD_JOURNAL_GROUP";
                    loCommand.CommandText = lcQuery;
                    loCommand.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 25, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                    loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE ", DbType.String, 20, lcJournalGroupType);
                    loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 55, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCommand, "@LOVERWRITE", DbType.String, 50, loIsOverwrite);

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
                    loConn = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM04500UploadErrorValidateDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<GSM04500UploadErrorValidateDTO> loResult = null;
            DbConnection loConn = null;

            try
            {
                loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<GSM04500UploadErrorValidateDTO>(loDataTableResult).ToList();
              
                foreach (var item in loResult)
                {
                    item.Var_Exists = true;
                    item.Var_Selected = false;
                    item.Var_Overwrite = false;
                }
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
