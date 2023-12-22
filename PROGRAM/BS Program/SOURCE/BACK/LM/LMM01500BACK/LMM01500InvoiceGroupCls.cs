using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using LMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;

namespace LMM01500BACK
{
    public class LMM01500InvoiceGroupCls : R_BusinessObject<LMM01500InvoiceGroupDetailDTO>
    {


        public LMM01500PropertyListDTO GetPropertyList(LMM01500DBParam poParameter)
        {

            R_Exception loException = new R_Exception();
            LMM01500PropertyListDTO loReturn = new();
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                var loReturnTemp2 = R_Utility.R_ConvertTo<LMM01500PropertyDTO>(loReturnTemp);
                loReturn.Data = loReturnTemp2.ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<LMM01500InvoiceGroupDTO> GetInvoiceGroupList(LMM01500DBParam poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM01500InvoiceGroupDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_INVOICE_GROUP_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 25, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM01500InvoiceGroupDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        protected override LMM01500InvoiceGroupDetailDTO R_Display(LMM01500InvoiceGroupDetailDTO poEntity)
        {

            R_Exception loException = new R_Exception();
            LMM01500InvoiceGroupDetailDTO loReturn = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_LM_GET_INVOICE_GROUP";
                loDb = new R_Db();
                var loCommand = loDb.GetCommand();
                var loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 20, poEntity.CINVGRP_CODE);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loReturn = R_Utility.R_ConvertTo<LMM01500InvoiceGroupDetailDTO>(loReturnTemp).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(LMM01500InvoiceGroupDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb;
            DbCommand loCommand = null;
            DbConnection loConn = null;
            loDb = new R_Db();
            string lcStorageId = "";
            try
            {
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);

                //SAVE DATA
                SaveDataSP(poNewEntity, poCRUDMode, loConn);
                //VALIDATE IS DATA EXIST

                if (poNewEntity.LINVOICE_TEMPLATE && !poNewEntity.LBY_DEPARTMENT)
                {
                    
                    lcStorageId= ValidateAlreadyExists(poNewEntity, loConn);
                    poNewEntity.CSTORAGE_ID = lcStorageId;

                    //SAVE TO CLOUD
                    SaveFiletoCloud(poNewEntity, poCRUDMode, loConn);

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
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loException.ThrowExceptionIfErrors();
        }
        private void SaveDataSP(LMM01500InvoiceGroupDetailDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            LMM01500InvoiceGroupDetailDTO loResult = null;
            string lcAction = "";

            try
            {
                //Set Action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                loConn = poConnection;
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poNewEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poNewEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poNewEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poNewEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poNewEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poNewEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

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
        private string ValidateAlreadyExists(LMM01500InvoiceGroupDetailDTO poNewEntity, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string? lcStorageId= null;

            try
            {
                loConn = poConnection;
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT CSTORAGE_ID FROM LMM_INVGRP (NOLOCK) " +
                          "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                          "AND CPROPERTY_ID = @CPROPERTY_ID " +
                          "AND CINVGRP_CODE = @CINVGRP_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                 var loTemp= R_Utility.R_ConvertTo<LMM01500InvoiceGroupDetailDTO>(loDataTable).FirstOrDefault();
                 lcStorageId = loTemp.CSTORAGE_ID;
                if (lcStorageId == null)
                {
                    lcStorageId = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return lcStorageId;
        }
        private void SaveFiletoCloud(LMM01500InvoiceGroupDetailDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = poConnection;
                loConnAttr = loDb.GetConnectionAttribute();
                loCmd = loDb.GetCommand();

                if (string.IsNullOrEmpty(poNewEntity.CSTORAGE_ID))
                {
                    //Add and create Storage ID
                    R_AddParameter loAddParameter;

                    loAddParameter = new R_AddParameter()
                    {
                        StorageType = R_EStorageType.Cloud,
                        ProviderCloudStorage = R_EProviderForCloudStorage.azure,
                        FileName = poNewEntity.CFileName,
                        FileExtension = poNewEntity.CFileExtension,
                        UploadData = poNewEntity.OData,
                        UserId = poNewEntity.CUSER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                            CDATA_TYPE = "STORAGE_DATA_TABLE",
                            CKEY01 = poNewEntity.CPROPERTY_ID,
                            CKEY02 = poNewEntity.CINVGRP_CODE,
                        }
                    };
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                    //Set Storage ID CSTORAGE_ID
                    poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;

                    lcQuery = "UPDATE LMM_INVGRP SET CSTORAGE_ID = @CSTORAGE_ID " +
                              "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                              "AND CPROPERTY_ID = @CPROPERTY_ID " +
                              "AND CINVGRP_CODE = @CINVGRP_CODE ";
                    loCmd.CommandText = lcQuery;
                    loCmd.CommandType = CommandType.Text;

                    loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 50, poNewEntity.CSTORAGE_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                    loDb.SqlExecNonQuery(loConn, loCmd, true);
                }
                else
                {
                    //UPDATE INVOICE FILE
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poNewEntity.CSTORAGE_ID,
                        UploadData = poNewEntity.OData,
                        UserId = poNewEntity.CUSER_ID,
                        //mostly tambahkan nama dan file extension baru
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileName = poNewEntity.CFileName.Trim(),
                            FileExtension = poNewEntity.CFileExtension.Trim()
                        }
                    };
                    R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(LMM01500InvoiceGroupDetailDTO poEntity)
        {
            var loException = new R_Exception();
            DbCommand loCommand = null;
            DbConnection loConn = null;
            R_DeleteParameter loDeleteParameter;
            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                string lcAction = "DELETE";


                if (!String.IsNullOrEmpty(poEntity.CSTORAGE_ID))
                {
                    loDeleteParameter = new R_DeleteParameter()
                    {
                        StorageId = poEntity.CSTORAGE_ID,
                        UserId = poEntity.CUSER_ID
                    };
                    R_StorageUtility.DeleteFile(loDeleteParameter, "R_DefaultConnectionString");
                }

                var lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CINVGRP_NAME", DbType.String, 100, poEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CSEQUENCE", DbType.String, 6, poEntity.CSEQUENCE);

                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_DUE_MODE", DbType.String, 2, poEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_GROUP_MODE", DbType.String, 2, poEntity.CINVOICE_GROUP_MODE);

                loDb.R_AddCommandParameter(loCommand, "@IDUE_DAYS ", DbType.Int32, 512, poEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCommand, "@IFIXED_DUE_DATE", DbType.Int32, 512, poEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@ILIMIT_INVOICE_DATE ", DbType.Int32, 512, poEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IBEFORE_LIMIT_INVOICE_DATE ", DbType.Int32, 512, poEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 512, poEntity.IAFTER_LIMIT_INVOICE_DATE);

                loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 2, poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 2, poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 2, poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCommand, "LUSE_STAMP", DbType.Boolean, 2, poEntity.LUSE_STAMP);

                loDb.R_AddCommandParameter(loCommand, "@CSTAMP_ADD_ID", DbType.String, 20, poEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION ", DbType.Int32, Int32.MaxValue, poEntity.CDESCRIPTION);

                loDb.R_AddCommandParameter(loCommand, "@LBY_DEPARTMENT", DbType.Boolean, 2, poEntity.LBY_DEPARTMENT);

                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

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
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}