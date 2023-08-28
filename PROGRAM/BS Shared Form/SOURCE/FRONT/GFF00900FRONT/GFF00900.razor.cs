using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using GFF00900Model;
using GFF00900Model.ViewModel;
using GFF00900COMMON.DTOs;
using System;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Components;
using R_CrossPlatformSecurity;
using R_BlazorFrontEnd.Helpers;
using BlazorClientHelper;
using System.Linq;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace GFF00900FRONT
{   
    public partial class GFF00900 : R_Page
    {
        private GFF00900ViewModel loViewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] R_ISymmetricJSProvider _encryptProvider { get; set; }

        private bool ReasonVisibility = true;

        private bool AccessValidationVisibility = true;

        private bool LayoutVisibility = true;

        private int mode = 0;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
                {
                loViewModel.loParameter.ACTION_CODE = (string)poParameter;
                loViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = (string)poParameter;

                R_Exception loException = new R_Exception();
                try
                {
                    await loViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();
                    if (loViewModel.loRspActivityValidityResult.Data.Any(x => x.CAPPROVAL_USER.Equals(_clientHelper.UserId, StringComparison.OrdinalIgnoreCase)))
                    {
                        loViewModel.loSelectedUser = loViewModel.loRspActivityValidityResult.Data.
                            Where(x => x.CAPPROVAL_USER.Equals(_clientHelper.UserId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                        mode = loViewModel.loSelectedUser.IAPPROVAL_MODE;
                      
                        LayoutVisibility = false;
                        ReasonVisibility = false;
                    }
                    else
                    {
                        var loValidate = await R_MessageBox.Show("", "User Not Allowed", R_eMessageBoxButtonType.OK);
                        if (loValidate == R_eMessageBoxResult.OK)
                        {
                            await this.Close(true, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                loException.ThrowExceptionIfErrors();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnOKReason()
        {
            if (string.IsNullOrWhiteSpace(loViewModel.DETAIL_ACTION))
            {
                var loValidate = await R_MessageBox.Show("", "Please Input Reason First", R_eMessageBoxButtonType.OK);
            }
            else
            {
                if (mode == 2)
                {
                    ReasonVisibility = true;
                    AccessValidationVisibility = false;
                }
            }
        }

        private async Task OnCancelReason()
        {
            await this.Close(true, false);
        }

        private async Task OnOK()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcEncryptedPassword = await _encryptProvider.TextEncrypt(loViewModel.loParameter.PASSWORD, loViewModel.loParameter.USER);
                loViewModel.loParameter.PASSWORD = lcEncryptedPassword;
                await loViewModel.UsernameAndPasswordValidationMethod();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            await this.Close(true, true);
        }

        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
    }
}
