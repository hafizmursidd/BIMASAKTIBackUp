using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Windows.Input;
using LMM06000Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM06000Back
{
    public class LMM06000Cls : R_BusinessObject<LMM06000BillingRuleDetailDTO>
    {
        protected override LMM06000BillingRuleDetailDTO R_Display(LMM06000BillingRuleDetailDTO poEntity)
        {
            R_Exception loEexception = new R_Exception();
            LMM06000BillingRuleDetailDTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = @"RSP_LM_GET_UNIT_TYPE_BILLING_RULE_DT";
                loDb = new R_Db();
                var loCmd = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 20, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 20, poEntity.CBILLING_RULE_CODE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, false);
                loReturn = R_Utility.R_ConvertTo<LMM06000BillingRuleDetailDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEexception.Add(ex);
            }
        EndBlock:
            loEexception.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override void R_Saving(LMM06000BillingRuleDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCmd = loDb.GetCommand();

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
                lcQuery = "RSP_LM_MAINTAIN_UNIT_TYPE_BILLING_RULE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 20, poNewEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 50, poNewEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_NAME", DbType.String, 100, poNewEntity.CBILLING_RULE_NAME);

                loDb.R_AddCommandParameter(loCmd, "@LBOOKING_FEE", DbType.Boolean, 2, poNewEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCmd, "@CBOOKING_FEE_CHARGES_ID", DbType.String, 50, poNewEntity.CBOOKING_FEE_CHARGE_ID);

                loDb.R_AddCommandParameter(loCmd, "@LWITH_DP", DbType.Boolean, 2, poNewEntity.LWITH_DP);
                loDb.R_AddCommandParameter(loCmd, "@IDP_PERCENTAGE", DbType.Int32, 25, poNewEntity.IDP_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IDP_INTERVAL", DbType.Int32, 25, poNewEntity.IDP_INTERVAL);
                loDb.R_AddCommandParameter(loCmd, "@CDP_PERIOD_MODE", DbType.String, 50, poNewEntity.CDP_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CDP_CHARGE_ID", DbType.String, 50, poNewEntity.CDP_CHARGE_ID);

                loDb.R_AddCommandParameter(loCmd, "@LINSTALLMENT", DbType.Boolean, 2, poNewEntity.LINSTALLMENT);
                loDb.R_AddCommandParameter(loCmd, "@IINSTALLMENT_PERCENTAGE", DbType.Int32, 50, poNewEntity.IINSTALLMENT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IINSTALLMENT_INTERVAL", DbType.Int32, 50, poNewEntity.IINSTALLMENT_INTERVAL);
                loDb.R_AddCommandParameter(loCmd, "@CINSTALLMENT_PERIOD_MODE", DbType.String, 50, poNewEntity.CINSTALLMENT_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINSTALLMENT_CHARGE_ID", DbType.String, 50, poNewEntity.CINSTALLMENT_CHARGE_ID);


                loDb.R_AddCommandParameter(loCmd, "@LBANK_CREDIT", DbType.Boolean, 5, poNewEntity.LBANK_CREDIT);
                loDb.R_AddCommandParameter(loCmd, "@IBANK_CREDIT_PERCENTAGE", DbType.Int32, 50, poNewEntity.IBANK_CREDIT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IBANK_CREDIT_INTERVAL", DbType.Int32, 50, poNewEntity.IBANK_CREDIT_INTERVAL);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 2, poNewEntity.LACTIVE);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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

        protected override void R_Deleting(LMM06000BillingRuleDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            DbCommand loCmd;
            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCmd = loDb.GetCommand();
                string lcAction = "DELETE";

                var lcQuery = "RSP_LM_MAINTAIN_UNIT_TYPE_BILLING_RULE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 20, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 50, poEntity.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_NAME", DbType.String, 50, poEntity.CBILLING_RULE_NAME);

                loDb.R_AddCommandParameter(loCmd, "@LBOOKING_FEE", DbType.Boolean, 2, poEntity.LBOOKING_FEE);
                loDb.R_AddCommandParameter(loCmd, "@CBOOKING_FEE_CHARGES_ID", DbType.String, 50, poEntity.CBOOKING_FEE_CHARGE_ID);

                loDb.R_AddCommandParameter(loCmd, "@LWITH_DP", DbType.Boolean, 2, poEntity.LWITH_DP);
                loDb.R_AddCommandParameter(loCmd, "@IDP_PERCENTAGE", DbType.Int32, 25, poEntity.IDP_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IDP_INTERVAL", DbType.Int32, 25, poEntity.IDP_INTERVAL);
                loDb.R_AddCommandParameter(loCmd, "@CDP_PERIOD_MODE", DbType.String, 50, poEntity.CDP_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CDP_CHARGE_ID", DbType.String, 50, poEntity.CDP_PERIOD_MODE);

                loDb.R_AddCommandParameter(loCmd, "@LINSTALLMENT", DbType.Boolean, 2, poEntity.LINSTALLMENT);
                loDb.R_AddCommandParameter(loCmd, "@IINSTALLMENT_PERCENTAGE", DbType.Int32, 50, poEntity.IINSTALLMENT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IINSTALLMENT_INTERVAL", DbType.Int32, 50, poEntity.IINSTALLMENT_INTERVAL);
                loDb.R_AddCommandParameter(loCmd, "@CINSTALLMENT_PERIOD_MODE", DbType.String, 50, poEntity.CINSTALLMENT_PERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINSTALLMENT_CHARGE_ID", DbType.String, 50, poEntity.CDP_PERIOD_MODE);

                loDb.R_AddCommandParameter(loCmd, "@LBANK_CREDIT", DbType.Boolean, 5, poEntity.LBANK_CREDIT);
                loDb.R_AddCommandParameter(loCmd, "@IBANK_CREDIT_PERCENTAGE", DbType.Int32, 50, poEntity.IBANK_CREDIT_PERCENTAGE);
                loDb.R_AddCommandParameter(loCmd, "@IBANK_CREDIT_INTERVAL", DbType.Int32, 50, poEntity.IBANK_CREDIT_INTERVAL);

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
                
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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

        public List<LMM06000BillingRuleDTO> BillingRuleList(LMM06000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM06000BillingRuleDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                var lcQuery = @"RSP_LM_GET_UNIT_TYPE_BILLING_RULE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 20, poParameter.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE_ONLY", DbType.Boolean, 2, poParameter.LACTIVE_ONLY);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM06000BillingRuleDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        public List<LMM06000PropertyDTO> GetAllPropertyList(LMM06000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM06000PropertyDTO> loReturn = null;
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

                loReturn = R_Utility.R_ConvertTo<LMM06000PropertyDTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<LMM06000UnitTypeDTO> GetAllUnitTypeList(LMM06000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM06000UnitTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_UNIT_TYPE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, 50, poParameter.CUNIT_TYPE_CATEGORY_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM06000UnitTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<LMM06000PeriodDTO> GetAllPeriodList(LMM06000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM06000PeriodDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            string Company = poParameter.CCOMPANY_ID;
            string Culture = poParameter.CULTURE;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = "SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', @CCOMPANY, '_PERIOD_MODE', '', @CULTURE )";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCommand, "CCOMPANY", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CULTURE", DbType.String, 255, poParameter.CULTURE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<LMM06000PeriodDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public void SetActiveInactiveDb(LMM06000ActiveInactiveDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_Db loDb;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_ACTIVE_INACTIVE_UNIT_TYPE_BILLING_RULE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poParameter.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poParameter.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_RULE_CODE", DbType.String, 50, poParameter.CBILLING_RULE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}