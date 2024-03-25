using LMM03700Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03700Back;
using LMM03700Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM03700Controller : ControllerBase, ILMM03700
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TenantClassificationGroupDTO> poParameter)
        {

            R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID);
                loRtn = new R_ServiceGetRecordResultDTO<TenantClassificationGroupDTO>();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<TenantClassificationGroupDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TenantClassificationGroupDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IAsyncEnumerable<TenantClassificationGroupDTO> GetTenantClassGroupList()
        {

            R_Exception loException = new R_Exception();
            List<TenantClassificationGroupDTO> loRtnTemp = null;
            LMM03700Cls loCls;
            try
            {
                loCls = new LMM03700Cls();
                loRtnTemp = loCls.GetTenantCategoryGroupList(new TenantClassificationGroupDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM03700ContextConstant.CPROPERTY_ID),
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return StreamHelper(loRtnTemp);
        }
        private async IAsyncEnumerable<T> StreamHelper<T>(List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                yield return entity;
            }
        }
    }
}
