﻿using GSM06500Common;
using GSM06500Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GSM06500Front
{
    public partial class GSM06500
    {
        private GSM06500ViewModel PaymentTermViewModel = new();
        private R_ConductorGrid _conGridPaymentRef;
        private R_Grid<GSM06500DTO> _gridRef;
        private R_Conductor _conductorRef;
        private int lnCountPage = 15;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await PaymentTermViewModel.GetPropertyList();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                PaymentTermViewModel.PropertyValueContext = lsProperty;
                 _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await PaymentTermViewModel.GetAllTermOfPaymentAsync();
               
                eventArgs.ListEntityResult = PaymentTermViewModel.PaymentOfTermList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                eventArgs.Result = await PaymentTermViewModel.GetTermOfPaymentOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                await PaymentTermViewModel.DeleteTermOfPayment(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06500DTO)eventArgs.Data;
                await PaymentTermViewModel.SaveTermOfPayment(loParam, eventArgs.ConductorMode);

                eventArgs.Result = PaymentTermViewModel.PaymentOfTerm;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void  ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PaymentTermViewModel.ValidationFieldEmpty((GSM06500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            R_DisplayException(loEx);
        }
    }
}