using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseHeaderReportCommon.BaseHeader;
using BaseHeaderReportCommon.Model;
using GLR00300Common.GLR00300Print;

namespace GLR00300Common.Model
{
    public static class GenerateDataModel
    {
        public static GLR00300AccountTrialBalanceResultDTO DefaultData()
        {
            GLR00300AccountTrialBalanceResultDTO loData = new GLR00300AccountTrialBalanceResultDTO()
            {
                Title = "Account Trial Balance",
                Header = new GLR00300HeaderAccountTrialBalanceDTO()
                {
                    CPERIOD = "Period",
                    CFROM_ACCOUNT_NO = "123.000.000",
                    CTO_ACCOUNT_NO = "456.000.000",
                    CFROM_CENTER_CODE = "123.000.000",
                    CTO_CENTER_CODE = "456.000.000",
                    CTB_TYPE_NAME = "Normal",
                    CCURRENCY = "IDR",
                    CJOURNAL_ADJ_MODE_NAME = "SPLIT",
                    CPRINT_METHOD_NAME = "SUPRESS NO TRANSACTION",
                    CBUDGET_NO = " - "
                },
                Column = new AccountTrialBalanceColumnDTO(),
                DataAccountTrialBalance = new List<GLR00300_DataDetail_AccountTrialBalance>()
            };

            List<GLR00300_DataDetail_AccountTrialBalance> loCollection = new List<GLR00300_DataDetail_AccountTrialBalance>();

            for (int i = 0; i < 20; i++)
            {
                loCollection.Add(new GLR00300_DataDetail_AccountTrialBalance()
                {
                    CGLACCOUNT_NO = $"15.000.1.00{i}",
                    CGLACCOUNT_NAME = $"ELECTRICAL NEW SYSTEM {i}",
                    CDBCR = "D",
                    CBSIS = "BS",
                    NBEGIN_BALANCE = 900000m* i * 1700m,
                    NCREDIT = 400000m + i * 1200m,
                    NDEBIT = 500000m + i * 2700m,
                    NDEBIT_ADJ = 600000m + i * 3700m,
                    NCREDIT_ADJ = 700000m + i * 4700m,
                    NEND_BALANCE = 800000m + i * 5999m,
                    NBUDGET = 800008m * 1000 * i
                }
                );
            }

            loData.DataAccountTrialBalance = loCollection;


            return loData;
        }

        public static GLR00300AccountTrialBalanceResultWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Account Trial Balance",
                CUSER_ID = "HMC"
            };

            GLR00300AccountTrialBalanceResultWithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResultWithBaseHeaderDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData(loParam).BaseHeaderData;
            loRtn.GLR00300_ACCOUNT_TRIAL_BALANCE_RESULT_DATA = DefaultData();

            return loRtn;
        }


        public static GLR00300AccountTrialBalanceResultFormat_EtoH_DTO DefaultData_Format_EtoH()
        {
            GLR00300AccountTrialBalanceResultFormat_EtoH_DTO loReturn =
                new GLR00300AccountTrialBalanceResultFormat_EtoH_DTO()
                {
                    Title = "Account Trial Balance",
                    Header = new GLR00300HeaderAccountTrialBalanceDTO()
                    {
                        CPERIOD = "Period",
                        CFROM_ACCOUNT_NO = "123.000.000",
                        CTO_ACCOUNT_NO = "456.000.000",
                        CFROM_CENTER_CODE = "123.000.000",
                        CTO_CENTER_CODE = "456.000.000",
                        CTB_TYPE_NAME = "Normal",
                        CCURRENCY = "IDR",
                        CJOURNAL_ADJ_MODE_NAME = "SPLIT",
                        CPRINT_METHOD_NAME = "SUPRESS NO TRANSACTION",
                        CBUDGET_NO = " - "
                    },
                    Column = new AccountTrialBalanceColumnDTO(),
                };

            List<GLR00300_DataDetail_AccountTrialBalance> loCollectionData = new List<GLR00300_DataDetail_AccountTrialBalance>();

            //GENERATE DATA DUMMY
            for (int i = 1; i < 3; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    loCollectionData.Add(new GLR00300_DataDetail_AccountTrialBalance()
                    {
                        CGLACCOUNT_NO = $"15.000.1.00{j}",
                        CGLACCOUNT_NAME = $"ELECTRICAL NEW SYSTEM {j}",
                        CDBCR = "D",
                        CBSIS = "BS",
                        CCENTER = $"712 -  Condo CGR {i}",
                        NBEGIN_BALANCE = 900000m * i * 17000m,
                        NCREDIT =400000m * i * 1100m,
                        NDEBIT = 500000m * i * 9700m,
                        NDEBIT_ADJ = 600000m * i * 3700m,
                        NCREDIT_ADJ = 700000m * i * 4700m,
                        NEND_BALANCE = 800000m * i * 5700m,
                        NBUDGET =80000.8m * 1000 * i
                    }
                    );
                }
            }
            //Grouping Data From DB to Ready to Display on Report
            var loTempData = loCollectionData
                .GroupBy(data => new
                {
                    data.CGLACCOUNT_NO,
                    data.CGLACCOUNT_NAME,
                    data.CDBCR,
                    data.CBSIS,
                }).Select(dataDetail => new GLRR00300DataAccountTrialBalance
                {
                    CGLACCOUNT_NO = dataDetail.Key.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = dataDetail.Key.CGLACCOUNT_NAME,
                    CDBCR = dataDetail.Key.CDBCR,
                    CBSIS = dataDetail.Key.CBSIS,
                    DataDetail = dataDetail.Select
                        (itemDetail => new GLRR00300DataDetailAccountTrialBalance
                        {
                            CCENTER = itemDetail.CCENTER,
                            NBEGIN_BALANCE = itemDetail.NBEGIN_BALANCE,
                            NCREDIT = itemDetail.NCREDIT,
                            NDEBIT = itemDetail.NDEBIT,
                            NDEBIT_ADJ = itemDetail.NDEBIT_ADJ,
                            NCREDIT_ADJ = itemDetail.NCREDIT_ADJ,
                            NEND_BALANCE = itemDetail.NEND_BALANCE,
                            NBUDGET = itemDetail.NBUDGET,
                        }).ToList()
                }).ToList();

            loReturn.Data = loTempData;


            return loReturn;
        }

        public static GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO DefaultDataWithHeaderFormat_EtoH()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Account Trial Balance",
                CUSER_ID = "HMC"
            };

            GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO loRtn = new GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData(loParam).BaseHeaderData;
            loRtn.GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat = DefaultData_Format_EtoH();

            return loRtn;
        }


    }
}
