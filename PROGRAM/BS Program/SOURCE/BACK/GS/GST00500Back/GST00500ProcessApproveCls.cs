using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GST00500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GST00500Back
{
    public class GST00500ProcessApproveCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand loCommand = null;
            bool llStatusApprove;
            List<GST00500ApprovalTransactionDTO> listApprovalTransactionReturn = new();
            string CCOMPANYID = poBatchProcessPar.Key.COMPANY_ID;
            string CUSERID = poBatchProcessPar.Key.USER_ID;
            string CGUID_ID = poBatchProcessPar.Key.KEY_GUID;
            int Var_Step = 0;
            string loStatusFinish = null;

            try
            {
                loCommand = loDb.GetCommand();

                var poListTransaction =
                    R_NetCoreUtility.R_DeserializeObjectFromByte<List<GST00500DTO>>(poBatchProcessPar.BigObject);

                lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, "Start Processing Approve");
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
                            llStatusApprove = UpdateEachApprovalStatus(item, loDb); //kirim koneksi yaitu LODB
                            //llStatusApprove = false;

                            if (llStatusApprove == false)
                            {
                                lotemp.LSUCCESSED = llStatusApprove;
                                lotemp.CCOMPANY_ID = CCOMPANYID;
                                lotemp.CTRANSACTION_CODE = item.CTRANSACTION_CODE;
                                lotemp.CDEPT_CODE = item.CDEPT_CODE;
                                lotemp.CREFERENCE_NO = item.CREFERENCE_NO;

                                listApprovalTransactionReturn.Add(lotemp);
                                string lsError =
                                    string.Format(
                                        "Error on Transaction Code {0} with Reference No {1}  has been failed when update status Approved!",
                                        item.CTRANSACTION_CODE, item.CREFERENCE_NO);
                               
                                //Send to Catch
                                throw new Exception(lsError) ;
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

                var flag = (listApprovalTransactionReturn.Count == 0) ? 1 : 9;

                //Chech if there is error on the process

                DbConnection loConn = null;
                try
                {
                    if (flag != 1)
                    {
                        loConn = loDb.GetConnection();
                        loStatusFinish = "Finish Processing But Fail !";
                        //Declare temp table
                        lcQuery = $"CREATE TABLE #ErrorTransaction " +
                                  $"(CCOMPANY_ID VARCHAR(24), " +
                                  $"CTRANSACTION_CODE VARCHAR(80), " +
                                  $"CDEPT_CODE VARCHAR(80), " +
                                  $"CREFERENCE_NO VARCHAR(80)," +
                                  $"LSUCCESSED BIT )";

                        loCommand.CommandText = lcQuery;
                        loCommand = loDb.GetCommand();
                        loCommand.CommandType = CommandType.Text;

                        loDb.SqlExecNonQuery(lcQuery, loConn, false);
                        loDb.R_BulkInsert((SqlConnection)loConn, "#ErrorTransaction", listApprovalTransactionReturn);

                        var Queryexec =
                            $"RSP_ConvertTableToXML '{CCOMPANYID}', '{CUSERID}', '{CGUID_ID}','#ErrorTransaction', 1 ";
                        loCommand.CommandText = Queryexec;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);
                    }
                    else
                    {
                        loStatusFinish = "Finish Processing Approve!";
                    }
                    //Close The Process to inform framework
                    lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                    loCommand.CommandText = lcQuery;
                    loCommand.CommandType = CommandType.StoredProcedure;
                    loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50,
                        poBatchProcessPar.Key.KEY_GUID);
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


        private bool UpdateEachApprovalStatus(GST00500DTO poEntity, R_Db poDb)
        {
            var loEx = new R_Exception();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            string lcCmd;
            bool lbRtn = false;
            try
            {
                //QUERY DIBAWAH INI MASIH PERLU DIPERIKSA LAGI
                //Konfirmasi CTRANSACTIONSTATUS apakah menggunakan >= '01' AND <= '02' atau MENGGGUNAKAN In ('01','02')

                loConn = poDb.GetConnection();
                loCommand = poDb.GetCommand();
                lcCmd = string.Format(@"SELECT CCOMPANY_ID From {0} (Updlock) 
                                      Where CCOMPANY_ID = '{1}' And CTRANSACTION_CODE = '{2}' 
                                      And CDEPT_CODE = '{3}'And CREFERENCE_NO = '{4}' And CTRANSACTION_STATUS In ('01','02'); 
                                      
                                      UPDATE GST_APPROVAL_I SET CAPPROVAL_STATUS= '02' 
                                      FROM GST_APPROVAL_I A (NOLOCK) WHERE A.CCOMPANY_ID= '{1}' AND A.CTRANSACTION_CODE = '{5}' 
                                      AND A.CDEPT_CODE= '{3}' AND A.CREFERENCE_NO= '{4}' AND A.CUSER_ID = '{6}' 
                                      AND A.CAPPROVAL_STATUS= '01' ",
                                        poEntity.CTABLE_NAME,
                                        poEntity.CCOMPANY_ID,
                                        poEntity.CTRANSACTION_NAME,
                                        poEntity.CDEPT_CODE,
                                        poEntity.CREFERENCE_NO,
                                        poEntity.CTRANSACTION_CODE,
                                        poEntity.CUSER_ID);

                //lcCmd = $"Select CCOMPANY_ID From GST_APPROVAL_I  (Updlock) " +
                //        $"Where CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' And CTRANSACTION_CODE = '{poEntity.CTRANSACTION_NAME}' " +
                //        $"And CDEPT_CODE = '{poEntity.CDEPT_CODE}'And CREFERENCE_NO = '{poEntity.CREFERENCE_NO}' And CTRANSACTION_STATUS In (01,02) " +

                //        $"UPDATE GST_APPROVAL_I SET CAPPROVAL_STATUS= '02' " +
                //        $"FROM GST_APPROVAL_I A (NOLOCK) WHERE A.CCOMPANY_ID= '{poEntity.CCOMPANY_ID}' AND A.CTRANSACTION_CODE = '{poEntity.CTRANSACTION_CODE}' " +
                //        $"AND A.CDEPT_CODE= '{poEntity.CDEPT_CODE}' AND A.CREFERENCE_NO= '{poEntity.CREFERENCE_NO}' AND A.CUSER_ID = '{poEntity.CUSER_ID}' " +
                //        $"AND A.CAPPROVAL_STATUS= '01'";

                loCommand.CommandText = lcCmd;
                loCommand.CommandType = CommandType.Text;
                poDb.SqlExecNonQuery(loConn, loCommand, false);

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
