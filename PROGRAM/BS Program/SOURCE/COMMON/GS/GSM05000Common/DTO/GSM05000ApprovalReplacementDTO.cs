﻿using System;

namespace GSM05000Common.DTO
{
    public class GSM05000ApprovalReplacementDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CTRANSACTION_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_REPLACEMENT { get; set; }
        public string CUSER_REPLACEMENT_NAME { get; set; }
        public string CVALID_FROM { get; set; }
        public string CVALID_TO { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

        public DateTime DVALID_FROM { get; set; }
        public DateTime DVALID_TO { get; set; }
    }
}