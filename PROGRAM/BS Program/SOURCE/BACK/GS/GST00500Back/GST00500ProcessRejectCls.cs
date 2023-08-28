using GST00500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Text.Json;

namespace GST00500Back
{
    public class GST00500ProcessRejectCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand loCommand = null;
            bool llStatusReject;
            List<GST00500ApprovalTransactionDTO> listRejectTransactionReturn = new();
            string CCOMPANYID = poBatchProcessPar.Key.COMPANY_ID;
            string CUSERID = poBatchProcessPar.Key.USER_ID;
            string CGUID_ID = poBatchProcessPar.Key.KEY_GUID;
            int Var_Step = 0;
            int lnErrorCount = 0;
            string loStatusFinish = null;

            try
            {
                loCommand = loDb.GetCommand();

                var poListTransaction = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GST00500DTO>>(poBatchProcessPar.BigObject);

                //GET PARAMETER
                var loContext = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CREASON_CODE)).FirstOrDefault().Value;
                var loContext2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.TNOTES)).FirstOrDefault().Value;
                string CREASON_CODE = ((JsonElement)loContext).GetString();
                string TNOTES = ((JsonElement)loContext2).GetString();

                GST00500RejectTransactionDTO loRejectParam = new()
                {
                    CREASON_CODE = CREASON_CODE,
                    TNOTES = TNOTES
                };


                lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, "Start Processing Reject");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);
                loDb.SqlExecNonQuery(loDb.GetConnection(), loCommand, true);

                Var_Step = 1;
                foreach (GST00500DTO item in poListTransaction)
                {
                    try
                    {
                        GST00500ApprovalTransactionDTO lotemp = new();
                        item.CCOMPANY_ID = CCOMPANYID;
                        item.CUSER_ID = CUSERID;

                        using (TransactionScope TransScope = new TransactionScope(TransactionScopeOption.Required))
                        {
                            //kirim  LoDb
                            llStatusReject = UpdateEachRejectStatus(item, loRejectParam, loDb);
                            llStatusReject = false;

                            if (llStatusReject == false)
                            {
                                lotemp.LSUCCESSED = llStatusReject;
                                lotemp.CCOMPANY_ID = CCOMPANYID;
                                lotemp.CTRANSACTION_CODE = item.CTRANSACTION_CODE;
                                lotemp.CDEPT_CODE = item.CDEPT_CODE;
                                lotemp.CREFERENCE_NO = item.CREFERENCE_NO;

                                listRejectTransactionReturn.Add(lotemp);
                                string lsError =
                                    string.Format(
                                        "Error on Transaction Code {0} with Reference No {1}  has been failed when update status Canceled!",
                                        item.CTRANSACTION_CODE, item.CREFERENCE_NO);

                                //Send to Catch
                                throw new Exception(lsError);
                            }

                            TransScope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Rollback the transaction
                    }

                EndDetail:
                    Var_Step++;
                }

                var flag = (listRejectTransactionReturn.Count == 0) ? 1 : 9;

                //Chech if there is error on the process
                DbConnection loConn = null;
                try
                {
                    if (flag != 1)
                    {
                        loStatusFinish = "Finish Process Reject But Fail !";
                        //Declare temp table
                        lcQuery = $"CREATE TABLE #ErrorTransaction " +
                                  $"(CCOMPANY_ID VARCHAR(24), " +
                                  $"CTRANSACTION_CODE VARCHAR(80), " +
                                  $"CDEPT_CODE VARCHAR(80), " +
                                  $"CREFERENCE_NO VARCHAR(80)," +
                                  $"LSUCCESSED BIT )";

                        //Assign to temp table for get the detail error
                        loDb.SqlExecNonQuery(lcQuery, loConn, false);
                        loDb.R_BulkInsert((SqlConnection)loConn, "#ErrorTransaction", listRejectTransactionReturn);

                        var Queryexec = $"RSP_ConvertTableToXML '{CCOMPANYID}', '{CUSERID}', '{CGUID_ID}','#ErrorTransaction', 1 ";
                        loCommand.CommandText = Queryexec;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);
                    }
                    else
                    {
                        loStatusFinish = "Finish Processing Reject !";
                    }

                    //Close The Process to inform framework
                    lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                    loCommand.CommandText = lcQuery;
                    loCommand.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                    loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, loStatusFinish);
                    loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, flag);
                    loDb.SqlExecNonQuery(loConn, loCommand, true);
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
                        {
                            loConn.Close();
                        }

                        loConn.Dispose();
                        loConn = null;
                    }

                    if (loCommand != null)
                    {
                        loCommand.Dispose();
                        loCommand = null;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private bool UpdateEachRejectStatus(GST00500DTO poEntity, GST00500RejectTransactionDTO poParam, R_Db poDb)
        {
            var loEx = new R_Exception();
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCommand = null; ;
            string lcCmd;
            bool lbRtn = false;
            try
            {
                //QUeRY DIABaWAH INI MASIH PERLU DIPERIKSA LAGI
                loDb = poDb;
                loConn = loDb.GetConnection();
                loCommand = poDb.GetCommand();

                loDb.R_AddCommandParameter(loCommand, "CREASON_CODE", DbType.String, 50, poParam.CREASON_CODE);
                loDb.R_AddCommandParameter(loCommand, "TNOTES", DbType.String, 255, poParam.TNOTES);

                lcCmd = $"Select CCOMPANY_ID From {poEntity.CTABLE_NAME} (Updlock) " +
                        $"Where CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' And CTRANSACTION_CODE = '{poEntity.CTRANSACTION_CODE}' " +
                        $"And CDEPT_CODE = '{poEntity.CDEPT_CODE}' And CREFERENCE_NO = '{poEntity.CREFERENCE_NO}' And CTRANSACTION_STATUS In (01,02); " +
                        $"Update GST_APPROVAL_I " +
                        $"Set CAPPROVAL_STATUS= '03' , CREASON_CODE= @CREASON_CODE , " +
                        $"TNOTES= @TNOTES FROM GST_APPROVAL_I A (NOLOCK)";

                loCommand.CommandText = lcCmd;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConn, loCommand, false);
                lbRtn = true;
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
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                    loConn = null;
                }
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return lbRtn;
        }

    }
}
