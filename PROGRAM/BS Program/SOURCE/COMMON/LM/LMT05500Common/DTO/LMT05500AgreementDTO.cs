﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMT05500Common.DTO
{
    public class LMT05500AgreementDTO : LMT05500Header
    {
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CAGREEMENT_TYPE { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime?  DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
    }
}
