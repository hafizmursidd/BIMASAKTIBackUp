using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using LMM01500COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;

namespace LMM01500Model
{
    public class LMM01500InvoiceGroupDeptModel : R_BusinessObjectServiceClientBase<LMM01500InvoiceGrpDeptDetailDTO>,
        ILMM01500InvoiceGroupDept
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01500InvoiceGroupDept";
        private const string DEFAULT_MODULE = "LM";

        public LMM01500InvoiceGroupDeptModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM01500InvoiceGrpDeptDTO> GetInvoiceGroupDeptList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500InvoiceGroupDeptListDTO> GetInvoiceGroupDeptListModel()
        {
            var loEx = new R_Exception();
            LMM01500InvoiceGroupDeptListDTO loResult = new LMM01500InvoiceGroupDeptListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResultTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500InvoiceGrpDeptDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500InvoiceGroupDept.GetInvoiceGroupDeptList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.ListData = loResultTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }
    }
}
