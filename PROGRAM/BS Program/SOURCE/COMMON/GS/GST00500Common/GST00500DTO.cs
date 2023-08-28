using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GST00500Common
{
    public class GST00500DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public bool LSELECTED { get; set; } = false;
        public int VAR_SELECTED { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CTRANSACTION_CODE { get; set; }
        public string CTRANSACTION_STATUS_DESC { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREFERENCE_NO { get; set; }
        public string CREFERENCE_DATE { get; set; }
        public string CAPPROVAL_STATUS { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CTABLE_NAME { get; set; }
    }

    //public class GST00500InboxListDTO : R_APIResultBaseDTO
    //{
    //    public List<GST00500DTO> Data { get; set; }
    //}

}
