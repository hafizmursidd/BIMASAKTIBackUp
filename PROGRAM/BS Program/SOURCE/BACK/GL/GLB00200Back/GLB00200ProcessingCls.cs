﻿using GLB00200Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace GLB00200Back
{
    public class GLB00200ProcessingCls : R_IBatchProcess
    {

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    //cara 1
                   // throw new Exception("Connection to database failed");

                   //cara2
                   loException.Add("", "Error where Connection to database");
                   goto EndBlock;
                }
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Exception loExceptionDt;
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand loCommand = null;
            bool loStatusProcess;
            string lcStatusProcess = null;
            string loStatusFinish = null;
            DbConnection loConnection = null;
            string lcCompany;
            string lcUserId;
            string lcGuidId;
            int Var_Step = 0;
            int Var_Total;
            int lnErrorCount = 0;
            string lsError;
            string lcQueryMessage;

            var loTempListForProcess =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<GLB00200DTO>>(poBatchProcessPar.BigObject);

            try
            {
                await Task.Delay(100);

                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcGuidId = poBatchProcessPar.Key.KEY_GUID;
                Var_Total = loTempListForProcess.Count;

                loCommand = loDb.GetCommand();
                loConnection = loDb.GetConnection();

                lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, lcCompany);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, lcUserId);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, lcGuidId);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500,
                    "Start Processing Reversing Journal");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);

                loDb.SqlExecNonQuery(loConnection, loCommand, false);



                Var_Step = 1;
                foreach (var item in loTempListForProcess)
                {
                    loExceptionDt = new R_Exception();
                    try
                    {
                        lcStatusProcess = string.Format("Process data {0} of  {1} ..", Var_Step, Var_Total);
                        lcQueryMessage = string.Format(
                               "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                               Var_Step, lcStatusProcess);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);

                        //CALL METHOD TO EACH PROCESS JOURNAL
                        loStatusProcess = ProcessEachReversing(lcCompany, lcUserId, item, lcGuidId, loConnection);

                        //loStatusProcess = false;
                        if (loStatusProcess == false)
                        {
                            lnErrorCount += 1;

                            lsError = string.Format("Failed to process Master Ref. No. CREF_NO {0} !!", item.CREF_NO);
                        }
                        else
                        {
                            lsError = string.Format("Processing Master Ref. No. CREF_NO {0}: SUCCESSFUL!", item.CREF_NO);
                        }

                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            Var_Step, lsError);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);
                    }
                    catch (Exception ex)
                    {
                        loExceptionDt.Add(ex);
                    }
                    
                //UNHANDLED Error
                    if (loExceptionDt.Haserror)
                    {
                        //Lakukan penambahan untuk menulis error pada GST_UPLOAD_ERROR_STATUS
                        //try catch ini digunakan untuk cek apakah ada error per data

                        lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS (CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                         $"VALUES " +
                                         $"( '{lcCompany}', '{lcUserId}','{lcGuidId}', {Var_Step}, '{loExceptionDt.ErrorList.FirstOrDefault().ErrDescp}') ;";

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);


                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            Var_Step, loExceptionDt.ErrorList.FirstOrDefault().ErrDescp);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);
                    }

                    Var_Step += 1;
                }

                //Check if there is error on the process
                var flag = (lnErrorCount == 0) ? 1 : 9;

                if (flag == 1)
                {
                    loStatusFinish = "Finish Processing Reversing Journal!!";
                }
                else
                {
                    loStatusFinish = "Finish Processing Reversing Journal but fail !!";
                }

                //Close The Process to inform framework
                var lcQueryFinish =
                    $@"EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, '{Var_Step}', '{loStatusFinish}', '{flag}'";
                loCommand.CommandText = lcQueryFinish;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConnection, loCommand, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConnection != null)
                {
                    if (!(loConnection.State == ConnectionState.Closed))
                        loConnection.Close();
                    loConnection.Dispose();
                    loConnection = null;
                }

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                //Lakukan penambahan pada GST_UPLOAD_ERROR_STATUS untuk handle Try catch paling luar

                lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                 $"VALUES " +
                                 $"( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', {100}, '{loException.ErrorList[0].ErrDescp}' );";

                loCommand.CommandText = lcQueryMessage;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConnection, loCommand, false);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                   $"'{poBatchProcessPar.Key.USER_ID}', " +
                   $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                   $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
        public bool ProcessEachReversing(string Company, string UserId, GLB00200DTO item, string pcGuid, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            bool lbRtn = false;
            DataTable loTable = null;
            string lcQuery;
            try
            {
                loConn = poConnection;
                loCommand = loDb.GetCommand();

                lcQuery = "RSP_GL_PROCESS_REVERSING_JRN";

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, Company);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, UserId);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 25, item.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 25, item.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID", DbType.String, 50, item.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@INO", DbType.Int32, 100, item.INO);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 50, pcGuid);

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loTable = loDb.SqlExecQuery(loConn, loCommand, false);

                if (loTable.Rows[0].Field<int>(0) > 0)
                {
                    lbRtn = false;
                }
                else
                {
                    lbRtn = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
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
