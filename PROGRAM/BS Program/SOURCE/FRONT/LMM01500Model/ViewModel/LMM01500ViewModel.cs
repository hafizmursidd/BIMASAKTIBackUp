using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_Security.Encryption;

namespace LMM01500Model.ViewModel
{
    public class LMM01500ViewModel : R_ViewModel<LMM01500InvoiceGroupDetailDTO>
    {
        private LMM01500Model _model = new LMM01500Model();
        public List<LMM01500PropertyDTO> PropertyList { get; set; } = new List<LMM01500PropertyDTO>();
        public ObservableCollection<LMM01500InvoiceGroupDTO> InvoiceGroupList =
            new ObservableCollection<LMM01500InvoiceGroupDTO>();
        public LMM01500InvoiceGroupDetailDTO InvoiceGroupDetail { get; set; } = new LMM01500InvoiceGroupDetailDTO();
  public string PropertyValueContext = "";
        public string InvoiceGroupValue = "";
        public List<LMM01500InvoiceDueModeDTO> InvoiceDueMode { get; set; } = new List<LMM01500InvoiceDueModeDTO>
        { new LMM01500InvoiceDueModeDTO { CODE = "01", DESC = "Tenant" },
            new LMM01500InvoiceDueModeDTO { CODE = "02", DESC = "Invoice Group" } };

        public List<LMM01500InvoiceDueModeDTO> InvoiceGroupMode { get; set; } = new List<LMM01500InvoiceDueModeDTO>
        { new LMM01500InvoiceDueModeDTO { CODE = "01", DESC = "Due Days" },
            new LMM01500InvoiceDueModeDTO { CODE = "02", DESC = "Fix Due Date" },
            new LMM01500InvoiceDueModeDTO { CODE = "03", DESC = "Range Fix Due Date" } };

        public string InvoiceDueModeValue = "01";
        public string InvoiceGroupModeValue = "01";

        public bool _IsButtonAddEnable = false;
        public bool isInvoiceGroup;
        public string _selectedBank="";
        public string _selectedDeptCode = "";

        public LMM01500TabParamDTO _tabParam = new LMM01500TabParamDTO();
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPropertyAsyncModel();
                PropertyList = loResult.Data;
                PropertyValueContext = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetInvoiceGroupList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
               
                var loResult = await _model.GetInvoiceGroupAsyncModel();
                InvoiceGroupList = new ObservableCollection<LMM01500InvoiceGroupDTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        }

        public async Task<LMM01500InvoiceGroupDetailDTO> GetInvoiceGroupDetail(LMM01500InvoiceGroupDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500InvoiceGroupDetailDTO loResult = new LMM01500InvoiceGroupDetailDTO();
            try
            {
                var loParam = new LMM01500InvoiceGroupDetailDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPROPERTY_ID = PropertyValueContext,
                    CINVGRP_CODE = poEntity.CINVGRP_CODE,
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);

                //set value for param
                _tabParam.CPROPERTY_ID = loResult.CPROPERTY_ID;
                _tabParam.CINVGRP_CODE = loResult.CINVGRP_CODE;

                //set value to enable disable group box on detail
                InvoiceDueModeValue = loResult.CINVOICE_DUE_MODE;

                InvoiceGroupDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task Delete_InvoiceGroup(LMM01500InvoiceGroupDetailDTO poEntity)
        {
        }
        public async Task Save_InvoiceGroup(LMM01500InvoiceGroupDetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CPROPERTY_ID = PropertyValueContext;
                    poEntity.CINVOICE_DUE_MODE = InvoiceDueModeValue;
                    poEntity.CINVOICE_GROUP_MODE = InvoiceGroupModeValue;
                }
                
                var loResult = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                InvoiceGroupDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
