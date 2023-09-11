using System;
using System.Collections.Generic;
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
                DataAccountTrialBalance = new List<GLR00300DataAccountTrialBalance>()
            };

            List<GLR00300DataAccountTrialBalance> loCollection = new List<GLR00300DataAccountTrialBalance>();
           
            for (int i = 0; i < 20; i++)
            {
                loCollection.Add(new GLR00300DataAccountTrialBalance()
                    {
                        CGLACCOUNT_NO = $"15.000.1.00{i}",
                        CGLACCOUNT_NAME = $"ELECTRICAL NEW SYSTEM {i}",
                        CDBCR = "D",
                        CBSIS = "BS",
                        NBEGIN_BALANCE = 900000m + i * 1.7m,
                        NCREDIT = 400000m + i * 1.100m,
                        NDEBIT = 500000m + i * 2.700m,
                        NDEBIT_ADJ = 600000m + i * 3.700m,
                        NCREDIT_ADJ = 700000m + i * 4.700m,
                        NEND_BALANCE = 800000m + i * 5.700m,
                        NBUDGET = 80000.8m*1000*i

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
            loRtn.GLR00300AccountTrialBalanceResultData = DefaultData();

            return loRtn;
        }


    }
}
