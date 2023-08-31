﻿using System;

namespace GLR00300ReportCommon
{
    public class AccountTrialBalanceColumnDTO
    {
        public string Col_ACCOUNT_NO { get; set; } = "Account No.";
        public string Col_ACCOUNT_NAME { get; set; } = "Account Name";
        public string Col_D_C { get; set; } = "D/C";
        public string Col_BS_IS { get; set; } = "BS/IS";
        public string Col_BEG_BALANCE { get; set; } = "Beg. Balance";
        public string Col_DEBIT { get; set; } = "Debit";
        public string Col_CREDIT { get; set; } = "Credit";
        public string Col_DEBIT_ADJ { get; set; } = "Debit Adj.";
        public string Col_CREDIT_ADJ { get; set; } = "Credit Adj.";
        public string Col_END_BALANCE { get; set; } = "End Balance";
        public string Col_MTD_BUDGET { get; set; } = "Mtd Budget";
    }
}