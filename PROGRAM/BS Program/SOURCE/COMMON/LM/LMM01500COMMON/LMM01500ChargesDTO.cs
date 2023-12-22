using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public class LMM01500ChargesDTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CINVGRP_NAME { get; set; }
     //   public bool LTabEnalbleDept { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // + result
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; } = "";
        public string UNIT_UTILITY_CHARGE { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_TYPE_DESCR { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }

    }

    public class LMM01500ChargesListDTO : R_APIResultBaseDTO
    {
        public List<LMM01500ChargesDTO> Data { get; set; }
    }
}
