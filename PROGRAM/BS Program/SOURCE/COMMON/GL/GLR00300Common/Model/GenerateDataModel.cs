using System;
using System.Collections.Generic;
using System.Text;
using GLR00300Common.GLR00300Print;

namespace GLR00300Common.Model
{
    public static class GenerateDataModel
    {
        public static GLR00300AccountTrialBalanceResultDTO DefaultData()
        {
            GLR00300AccountTrialBalanceResultDTO loData = new GLR00300AccountTrialBalanceResultDTO()
            {
                Title = "ProductTitle",
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
           
            for (int i = 0; i < 5; i++)
            {
                loCollection.Add(new GLR00300DataAccountTrialBalance()
                    {
                        CGLACCOUNT_NO = $"15.000.1.00{i}",
                        CGLACCOUNT_NAME = $"Electrical System {i}",
                        CDBCR = "D",
                        CBSIS = "BS",
                        NBEGIN_BALANCE = 4000000m + i * 1.7m,
                        NCREDIT = 4000000m + i * 1.1m,
                        NDEBIT = 4000000m + i * 2.7m,
                        NDEBIT_ADJ = 4000000m + i * 3.7m,
                        NCREDIT_ADJ = 4000000m + i * 4.7m,
                        NEND_BALANCE = 4000000m + i * 5.7m

                    }
                );
            }

            loData.DataAccountTrialBalance = loCollection;


            return loData;
        }

        //public static GLR00300AccountTrialBalanceResultDTO DefaultDataWithHeader()
        //{
        //    //var loParam = new BaseHeaderDTO()
        //    //{
        //    //    CCOMPANY_NAME = "PT Realta Chackradarma",
        //    //    CPRINT_CODE = "001",
        //    //    CPRINT_NAME = "Center",
        //    //    CUSER_ID = "ERC",
        //    //};

        //    //GSM01500PrintCenterResultWithBaseHeaderPrintDTO loRtn = new GSM01500PrintCenterResultWithBaseHeaderPrintDTO();
        //    //loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData(loParam).BaseHeaderData;
        //    //loRtn.CenterData = GSM01500PrintCenterModelDummyData.DefaultDataCenter();

        //    //return loRtn;
        //}


    }
}
