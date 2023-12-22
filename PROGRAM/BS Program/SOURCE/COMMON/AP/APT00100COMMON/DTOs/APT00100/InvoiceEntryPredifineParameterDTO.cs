using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00100
{
    public class InvoiceEntryPredifineParameterDTO
    {
        public GetCompanyInfoDTO COMPANY_INFO { get; set; }
        public GetAPSystemParamDTO APSystemParam { get; set; }
        public string CREC_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        public string CTRANSACTION_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREFERENCE_NO { get; set; } = "";

    }
}
