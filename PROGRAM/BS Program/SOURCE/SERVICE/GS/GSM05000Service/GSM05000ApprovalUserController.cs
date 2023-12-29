using GSM05000Back;
using GSM05000Common.DTO;
using GSM05000Common.Interface;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000ApprovalUserController : ControllerBase, IGSM05000ApprovalUser
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> loRtn = new();

        try
        {
            var loCls = new GSM05000ApprovalUserCls();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> loRtn = null;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader(GSM05000TrxCodeParamsDTO poParams)
    {
        R_Exception loEx = new();
        GSM05000ApprovalHeaderDTO loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANS_CODE
            };

            loCls = new GSM05000ApprovalUserCls();
            
            loRtn = loCls.GSM05000GetApprovalHeader(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetApprovalListStream()
    {
        R_Exception loEx = new();
        IAsyncEnumerable<GSM05000ApprovalUserDTO> loRtn = null;
        List<GSM05000ApprovalUserDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
            };
            
            loCls = new GSM05000ApprovalUserCls();
            loResult = loCls.GSM05000GetApprovalUser(loDbPar);
            // loRtn = GetApprovalStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public string GSM05000ValidationForAction(GSM05000TrxDeptParamsDTO poParams)
    {
        R_Exception loEx = new();
        string loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
            };

            loCls = new GSM05000ApprovalUserCls();
            
            loRtn = loCls.GSM05000ValidationForAction(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartmentStream()
    {
        
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            
            loResult = loCls.GSM05000GetApprovalDepartment(loDbPar);
            // loRtn = GetApprovalDepartmentStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequenceStream(GSM05000TrxCodeParamsDTO poParams)
    {
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = poParams.CTRANS_CODE
            };

            loCls = new GSM05000ApprovalUserCls();
            loResult = loCls.GSM05000DepartmentChangeSequence(loDbPar);
            // loRtn = DepartmentChangeSequenceStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);

        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceDataStream()
    {
        R_Exception loEx = new();
        List<GSM05000ApprovalUserDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalUserDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE)
            };

            loCls = new GSM05000ApprovalUserCls();
            
            loResult = loCls.GSM05000GetUserSequenceData(loDbPar);
            // loRtn = GetUserSequenceDataStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            var loCls = new GSM05000ApprovalUserCls();

            poEntity.ForEach(x => x.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID);
            loCls.GSM05000UpdateSequence(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream()
    {
        
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID
                // CDEPT_CODE = poParams.CDEPT_CODE,
            };

            loCls = new GSM05000ApprovalUserCls();
            
            loResult = loCls.GSM05000LookupApprovalDepartment(loDbPar);
            // loRtn = LookupApprovalDepartmentStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    [HttpPost]
    public GSM05000SingleDTO<string> GSM05000CopyToApproval(GSM05000CopyToParamsDTO poParams)
    {
        R_Exception loEx = new();
        GSM05000SingleDTO<string> loReturn = new();
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
                CDEPT_CODE_TO = poParams.CDEPT_CODE_TO,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            loCls.GSM05000ApprovalCopyTo(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    [HttpPost]
    public GSM05000SingleDTO<string> GSM05000CopyFromApproval(GSM05000CopyFromParamsDTO poParams)
    {
        R_Exception loEx = new();
        GSM05000SingleDTO<string> loReturn = new();
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
                CDEPT_CODE_FROM = poParams.CDEPT_CODE_FROM,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            loCls.GSM05000ApprovalCopyFrom(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (T item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}