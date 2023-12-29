using System.Data;
using System.Data.Common;
using GSM05000Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back
{
    public class GSM05000TransactionCls : R_BusinessObject<GSM05000TransactionDetailDTO>
    {
        protected override GSM05000TransactionDetailDTO R_Display(GSM05000TransactionDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM05000TransactionDetailDTO loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = @"RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 30, poEntity.CTRANS_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM05000TransactionDetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(GSM05000TransactionDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(GSM05000TransactionDetailDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public List<GSM05000TransactionDTO> GetTransactionCodeList(GSM05000ParameterDb poParam)
        {
            R_Exception loEx = new R_Exception();
            List<GSM05000TransactionDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"RSP_GS_GET_TRANS_CODE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 30, poParam.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM05000TransactionDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public List<GSM05000DelimiterDTO> GetDelimiterList(GSM05000ParameterDb poParameterDb)
        {
            R_Exception loEx = new();
            List<GSM05000DelimiterDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery =
                    @$"SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', '{poParameterDb.CCOMPANY_ID}', '_GS_REFNO_DELIMITER', '', '{poParameterDb.CLANGUAGE_ID}')";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = R_Utility.R_ConvertTo<GSM05000DelimiterDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}