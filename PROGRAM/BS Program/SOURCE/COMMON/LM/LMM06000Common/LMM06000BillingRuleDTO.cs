﻿using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM06000Common
{
    public class LMM06000BillingRuleDTO
    {        
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public bool LACTIVE_ONLY { get; set; }

        public string CBILLING_RULE_CODE { get; set; }
        public string CBILLING_RULE_NAME { get; set; }

        public decimal NMIN_BOOKING_FEE { get; set; }
        public bool LBOOKING_FEE_OVERWRITE { get; set; }

        public int IDP_PERCENTAGE { get; set; }
        public int IDP_INTERVAL { get; set; }
        public string CDP_PERIOD_MODE { get; set; }
        public string CDP_PERIOD_MODE_DESCR { get; set; }
        
        public int IINSTALLMENT_PERCENTAGE { get; set; }
        public int IINSTALLMENT_INTERVAL { get; set; }
        public string CINSTALLMENT_PERIOD_MODE { get; set; }
        public string CINSTALLMENT_PERIOD_MODE_DESCR { get; set; }

        public int IBANK_CREDIT_PERCENTAGE { get; set; }
        public int IBANK_CREDIT_INTERVAL { get; set; }

        public bool LACTIVE{ get; set; }

        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        
    }

    public class LMM06000BillingRuleListDTO : R_APIResultBaseDTO
    {
        public List<LMM06000BillingRuleDTO> Data { get; set; }
    }
}
