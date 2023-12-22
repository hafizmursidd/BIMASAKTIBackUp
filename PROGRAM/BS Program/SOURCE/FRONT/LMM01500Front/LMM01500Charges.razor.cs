using BlazorClientHelper;
using LMM01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;

namespace LMM01500Front
{
    public partial class LMM01500Charges
    {
        private LMM01500ChargesViewModel _OtherChargesViewModel = new LMM01500ChargesViewModel();
        private R_Grid<LMM01500ChargesDTO> _gridOherCharges_Ref;
        private R_ConductorGrid _conductorOtherChargesRef;
        [Inject] IClientHelper clientHelper { get; set; }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _OtherChargesViewModel._TabParam = (LMM01500TabParamDTO)poParameter;
               await _gridOherCharges_Ref.R_RefreshGrid((LMM01500TabParamDTO)poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetChargesList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if ((LMM01500TabParamDTO)eventArgs.Parameter != null)

                {
                    await _OtherChargesViewModel.GetChargestList();
                    eventArgs.ListEntityResult = _OtherChargesViewModel.ChargesList;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecordCharges(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //var loParam = R_FrontUtility.ConvertObjectToObject<LMM01500InvoiceGrpDeptDetailDTO>(eventArgs.Data);
                //await _OtherChargesViewModel.GetInvoiceGroupDetail(loParam);
                //eventArgs.Result = _LMM01500InvoiceGrpDeptViewModel.InvoiceGroupDeptDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }   
}
