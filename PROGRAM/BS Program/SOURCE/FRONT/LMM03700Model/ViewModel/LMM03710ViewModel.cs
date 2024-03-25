﻿using LMM03700Common;
using LMM03700Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Model.ViewModel
{
    public class LMM03710ViewModel : R_ViewModel<TenantClassificationDTO>
    {

     //   private LMM03710Model _modelTenantClassGrp = new LMM03710Model();
        private LMM03710Model _modelTenantClass = new LMM03710Model();
        public ObservableCollection<TenantClassificationGroupDTO> _TenantClassificationGroupList { get; set; } = new ObservableCollection<TenantClassificationGroupDTO>();
        public ObservableCollection<TenantClassificationDTO> TenantClassList { get; set; } = new ObservableCollection<TenantClassificationDTO>();
        public ObservableCollection<TenantDTO> AssignedTenantList { get; set; } = new ObservableCollection<TenantDTO>();

        public TenantClassificationGroupDTO TenantClassGroup { get; set; } = new TenantClassificationGroupDTO();

        public List<PropertyDTO> _PropertyList { get; set; } = new List<PropertyDTO>();

        //assign
        public ObservableCollection<TenantDTO> AvailableTenantList { get; set; } = new ObservableCollection<TenantDTO>();
        public ObservableCollection<TenantDTO> SelectedTenantList { get; set; } = new ObservableCollection<TenantDTO>();

        //for move popup
        public ObservableCollection<TenantGridDTO> TenantToMoveList { get; set; } = new ObservableCollection<TenantGridDTO>();
        public List<TenantClassificationDTO> TenantClassListForMoveTenant { get; set; } = new List<TenantClassificationDTO>();
        public TenantClassificationDTO TenantClassForMoveTenant { get; set; } = new TenantClassificationDTO();
        //end-formove popup

        public string _propertyId { get; set; } = "";
        public bool _tab2IsActive { get; set; } = false;
        public string _tenantClassificationId { get; set; } = "";
        public string _tenantClassificationGroupId { get; set; } = "";

        //this string is using to move tenant
        public string _toTenantClassificationId = "";
        public string _fromTenantClassificationId = "";
        //end
        public TenantClassificationDTO TenantClass { get; set; } = new TenantClassificationDTO();


        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelTenantClass.GetPropertyListAsync();
                _PropertyList = loResult;
                _propertyId = _PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region TenantClassGroup
        public async Task GetTenantClassGroupList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                var loResult = await _modelTenantClass.GetTenantClassGroupList();
                _TenantClassificationGroupList = new ObservableCollection<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TenantClassificaiton
        public async Task GetTenantClassList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                TenantClassificationGroupDTO param = TenantClassGroup;

                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, param.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, param.CTENANT_CLASSIFICATION_GROUP_ID);
                var loResult = await _modelTenantClass.GetTenantClassificationListAsync();
                TenantClassList = new ObservableCollection<TenantClassificationDTO>(loResult);
                if (TenantClassList.Count == 0)
                {
                   // AssignedTenantList = new ObservableCollection<TenantDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantClassRecord(TenantClassificationDTO loParam)
        {
            loParam.CPROPERTY_ID = _propertyId;
            loParam.CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId;
            var loResult = await _modelTenantClass.R_ServiceGetRecordAsync(loParam);
            TenantClass = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(loResult);
        }
        public async Task SaveTenantClass(TenantClassificationDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                TenantClassificationDTO loResult = new TenantClassificationDTO();
                poNewEntity.CPROPERTY_ID = _propertyId;
                poNewEntity.CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId;
                loResult = await _modelTenantClass.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                TenantClass = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteTenantClass(TenantClassificationDTO loParam)
        {
            var loEx = new R_Exception();

            try
            {
                loParam.CPROPERTY_ID = _propertyId;
                loParam.CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId;
                await _modelTenantClass.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TenantList
        public async Task GetAssignedTenantList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID, _tenantClassificationId);
             //   R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, _tenantClassificationGroupId);
                var loResult = await _modelTenantClass.GetAssignedTenantListAsync();
                AssignedTenantList = new ObservableCollection<TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region AssignTenant
        public async Task GetTenantToAssignList(TenantGridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID, poParam.CTENANT_CLASSIFICATION_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, poParam.CTENANT_CLASSIFICATION_GROUP_ID);
                var loResult = await _modelTenantClass.GetTenantToAssigntListAsync();
                AvailableTenantList = new ObservableCollection<TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetSelectedTenantList(TenantGridDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID, poParam.CTENANT_CLASSIFICATION_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, poParam.CTENANT_CLASSIFICATION_GROUP_ID); ;
                var loResult = await _modelTenantClass.GetAssignedTenantListAsync();
                SelectedTenantList = new ObservableCollection<TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task AssignTenantProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _modelTenantClass.AssignTenantAsync(new TenantParamDTO()
                {
                    CPROPERTY_ID = _propertyId,
                    CTENANT_CLASSIFICATION_ID = _tenantClassificationId,
                    CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId,
                    //change to comma separator
                    CTENANT_ID_LIST_COMMA_SEPARATOR = string.Join(",", SelectedTenantList.Select(tenant => tenant.CTENANT_ID)),
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region MoveTenant
        public async Task GetTenantClassListForMove()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, _tenantClassificationGroupId);
                var loResult = await _modelTenantClass.GetTenantClassificationListAsync();
                if (loResult.Count != 0 || loResult != null)
                {
                    var loListTenantClassUnfilteredList = new List<TenantClassificationDTO>(loResult);
                    TenantClassListForMoveTenant = loListTenantClassUnfilteredList.Where(d => d.CTENANT_CLASSIFICATION_ID != _tenantClassificationId).ToList(); //filter tenant class list except recent tenant positiion
                    if (TenantClassListForMoveTenant.Count != 0)
                    {
                        _toTenantClassificationId = TenantClassListForMoveTenant.FirstOrDefault().CTENANT_CLASSIFICATION_ID; //set default selected destination tenant to move
                    }
                    else
                    {
                        _toTenantClassificationId = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantListToMove(TenantClassificationDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CTENANT_CLASSIFICATION_ID, poParam.CTENANT_CLASSIFICATION_ID);
                var loResult = await _modelTenantClass.GetTenanToMoveListAsync();
                var loResultWithSelectProperty = R_FrontUtility.ConvertCollectionToCollection<TenantGridDTO>(loResult);
                TenantToMoveList = new ObservableCollection<TenantGridDTO>(loResultWithSelectProperty);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task MoveTenant()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _modelTenantClass.MoveTenantAsync(new TenantMoveParamDTO()
                {
                    CPROPERTY_ID = _propertyId,
                    CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId,
                    CTO_TENANT_CLASSIFICATION_ID = _toTenantClassificationId,
                    CFROM_TENANT_CLASSIFICATION_ID = _fromTenantClassificationId,
                    CTENANT_ID_LIST_COMMA_SEPARATOR = string.Join(",", TenantToMoveList.Where(tenant => tenant.LCHECKED).Select(tenant => tenant.CTENANT_ID)),
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetTenantClassRecordForMove(TenantClassificationDTO loParam)
        {
            loParam.CPROPERTY_ID = _propertyId;
            loParam.CTENANT_CLASSIFICATION_GROUP_ID = _tenantClassificationGroupId;
            var loResult = await _modelTenantClass.R_ServiceGetRecordAsync(loParam);
            TenantClassForMoveTenant = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(loResult);
        }
        #endregion
    }
}
    