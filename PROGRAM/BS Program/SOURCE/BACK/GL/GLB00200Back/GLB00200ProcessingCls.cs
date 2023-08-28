using GLB00200Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace GLB00200Back
{
    public class GLB00200ProcessingCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {

            R_Exception loException = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCommand = loDb.GetCommand();
            var loTempListForProcess = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GLB00200DTO>>(poBatchProcessPar.BigObject);
            try
            {
                int Var_Step = 0;
                int Var_Total = loTempListForProcess.Count;
                int lnErrorCount = 0;
                loConn = loDb.GetConnection();

                lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, "Start Processing Reversing Journal");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);
                loDb.SqlExecNonQuery(loConn, loCommand, false);

                bool llStatusApprove;
                string Company = poBatchProcessPar.Key.COMPANY_ID;
                string UserId = poBatchProcessPar.Key.USER_ID;
                string GuidId = poBatchProcessPar.Key.KEY_GUID;

                try
                {

                    Var_Step = 1;
                    foreach (var item in loTempListForProcess)
                    {
                        try
                        {
                            using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                //WRITE PROCESS STATUS
                                // MessageProcess(Company, UserId, loConn, GuidId, Var_Step, Var_Total);

                                string lcStatusProcess =  string.Format("Process data {0} of  {1} ..", Var_Step, Var_Total);
                                var lcQueryMessage = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0", Var_Step, lcStatusProcess);

                                loCommand.CommandText = lcQueryMessage;
                                loCommand.CommandType = CommandType.Text;
                                loDb.SqlExecNonQuery(loConn, loCommand, false);

                                //PROCESS EACH REVERSING JOURNAL
                                 llStatusApprove = ProcessEachReversing(Company, UserId, loConn, item);
                                //llStatusApprove = false;
                                if (llStatusApprove == false)
                                {
                                    lnErrorCount += 1;
                                    loException.Add("", string.Format("Failed to process Master Ref. No. CREF_NO {0} !!", item.CREF_NO) );
                                    goto EndDetail;
                                }
                                TransScope.Complete();
                            }
                        }
                        catch (Exception ex)
                        {
                            loException.Add(ex);
                        }
                    EndDetail:
                        Var_Step += 1;
                    }

                    var flag = (lnErrorCount == 0) ? 1 : 9;

                    string loStatusFinish = null;
                    if (flag == 1)
                    {
                        loStatusFinish = "Finish Processing Reversing Journal!!";
                    }
                    else
                    {
                        loStatusFinish = "Finish Processing Reversing Journal but fail !!";
                    }

                    var lcQueryFinish = $@"EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, '{Var_Step}', '{loStatusFinish}', '{flag}'";
                    loCommand.CommandText = lcQueryFinish;
                    loCommand.CommandType = CommandType.Text;
                    loDb.SqlExecNonQuery(loConn, loCommand, true);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
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
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public bool ProcessEachReversing(string Company, string UserId, DbConnection poConnection, GLB00200DTO item)
        {
            var loEx = new R_Exception();
            DbConnection loConn = poConnection;
            DbCommand loCommand;
            R_Db loDb;
            bool lbRtn = false;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loCommand = loDb.GetCommand();

                lcQuery = "RSP_GL_PROCESS_REVERSING_JRN";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, Company);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, UserId);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 25, item.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 25, item.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID", DbType.String, 50, item.CREC_ID);

                loDb.SqlExecNonQuery(loConn, loCommand, false);
                lbRtn = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return lbRtn;
        }

        public void MessageProcess(string Company, string UserId, DbConnection poConnection, string GuidId, int Step, int Count)
        {
            var loEx = new R_Exception();
            string lcQuery;
            DbConnection loConn = poConnection;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                loCommand = loDb.GetCommand();

                lcQuery = @"RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, Company);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, UserId);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 20, GuidId);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, $"Process data {Step} of  {Count}!!");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);
                loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
    }

}
