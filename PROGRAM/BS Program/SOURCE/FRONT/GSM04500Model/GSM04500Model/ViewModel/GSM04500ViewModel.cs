﻿using GSM04500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GSM04500Model
{
    public class GSM04500ViewModel : R_ViewModel<GSM04500DTO>
    {
        private GSM04500Model _model = new GSM04500Model();
        private GSM04500ModelUploadTemplate _modelUploadTemplate = new GSM04500ModelUploadTemplate();
        public List<GSM04500PropertyDTO> PropertyList { get; set; } = new List<GSM04500PropertyDTO>();
        public List<GSM04500JournalGroupTypeDTO> JournalGroupTypeList { get; set; } = new List<GSM04500JournalGroupTypeDTO>();
        
        public ObservableCollection<GSM04500DTO> JournalGroupList = new ObservableCollection<GSM04500DTO>();
        public GSM04500DTO JournalGroup { get; set; } = new GSM04500DTO();

        public GSM04500DTO JournalGroupCurrent  = new GSM04500DTO();
        public GSM04500PropertyDTO CurrentProperty = new GSM04500PropertyDTO();

        public string PropertyValueContext = "";
        public string JournalGroupTypeValue = "";
        public string JournalGroupCodeValue = "";
        public bool DropdownGroupType = true;
        public bool DropdownProperty = true;
        public bool btnUploadEnable = false;

        public bool VisibleColumn_LACCRUAL = false;


        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPropertyListAsyncModel();
                PropertyList = loResult.Data;
                CurrentProperty = PropertyList[0];
                PropertyValueContext = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalGroupTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetJournalGroupTypeListAsyncModel();
                JournalGroupTypeList = loResult.Data;
                JournalGroupTypeValue = JournalGroupTypeList[0].CCODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetAllJournalAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                switch (JournalGroupTypeValue)
                {
                    case "10":
                    case "11":
                    case "40":
                        VisibleColumn_LACCRUAL = true;
                        break;
                    default:
                        VisibleColumn_LACCRUAL = false;
                        break;
                }

                var x = VisibleColumn_LACCRUAL;
                var loResult = await _model.GetAllJournalGroupListAsync(JournalGroupTypeValue, PropertyValueContext);
                JournalGroupList = new ObservableCollection<GSM04500DTO>(loResult.ListData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }


        public async Task<GSM04500DTO> GetGroupJournaltOneRecord(GSM04500DTO poProperty)
        {
            var loEx = new R_Exception();
            GSM04500DTO loResult = null;

            try
            {
                var loParam = new GSM04500DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CUSER_ID = poProperty.CUSER_ID,
                    CPROPERTY_ID = poProperty.CPROPERTY_ID,
                    CJRNGRP_TYPE = poProperty.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poProperty.CJRNGRP_CODE
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task DeleteOneRecordJournalGroup(GSM04500DTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM04500DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CPROPERTY_ID = poProperty.CPROPERTY_ID,
                    CJRNGRP_TYPE = poProperty.CJRNGRP_TYPE,
                    CJRNGRP_CODE = poProperty.CJRNGRP_CODE,
                    CJRNGRP_NAME = poProperty.CJRNGRP_NAME,
                    LACCRUAL = poProperty.LACCRUAL
                };
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM04500DTO> SaveJournalGroup(GSM04500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM04500DTO loResult = null;

            try
            {
                poNewEntity.CPROPERTY_ID = PropertyValueContext;
                poNewEntity.CJRNGRP_TYPE = JournalGroupTypeValue;
                loResult = await _model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                JournalGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        #region Template
        public async Task<GSM04500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM04500UploadFileDTO loResult = null;

            try
            {
                loResult = await _modelUploadTemplate.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
