using GSM06500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Windows.Input;
using System.Reflection.Metadata;

namespace GSM06500Back
{

    public class GSM06500Cls : R_BusinessObject<GSM06500DTO>
    {
        protected override void R_Deleting(GSM06500DTO poEntity)
        {
            var loEx = new R_Exception();
            DbCommand loCommand;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_GS_MAINTAIN_PAYMENT_TERM";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPAY_TERM_CODE", DbType.String, 50, poEntity.CPAY_TERM_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CPAY_TERM_NAME", DbType.String, 100, poEntity.CPAY_TERM_NAME);
                loDb.R_AddCommandParameter(loCommand, "@IPAY_TERM_DAYS", DbType.Int32, 99, poEntity.IPAY_TERM_DAYS);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override GSM06500DTO R_Display(GSM06500DTO poEntity)
        {
            R_Exception loEexception = new R_Exception();
            GSM06500DTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = @"RSP_GS_GET_PAYMENT_TERM_DETAIL";

                loDb = new R_Db();
                var loCmd = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPAY_TERM_CODE", DbType.String, 10, poEntity.CPAY_TERM_CODE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM06500DTO>(loReturnTemp).ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {

                loEexception.Add(ex);
            }
        EndBlock:
            loEexception.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override void R_Saving(GSM06500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                    default:
                        break;
                }
                lcQuery = "RSP_GS_MAINTAIN_PAYMENT_TERM";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CPROPERTY_ID", DbType.String, 10, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "CPAY_TERM_CODE", DbType.String, 8, poNewEntity.CPAY_TERM_CODE);
                loDb.R_AddCommandParameter(loCommand, "CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "CPAY_TERM_NAME", DbType.String, 100, poNewEntity.CPAY_TERM_NAME);
                loDb.R_AddCommandParameter(loCommand, "IPAY_TERM_DAYS", DbType.Int32, 999999, poNewEntity.IPAY_TERM_DAYS);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
                }
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM06500DTO> TERM_OF_LIST(GSM06500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM06500DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PAYMENT_TERM_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM06500DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<GSM06500PropertyDTO> GetAllPropertyList(GSM06500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM06500PropertyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM06500PropertyDTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
}