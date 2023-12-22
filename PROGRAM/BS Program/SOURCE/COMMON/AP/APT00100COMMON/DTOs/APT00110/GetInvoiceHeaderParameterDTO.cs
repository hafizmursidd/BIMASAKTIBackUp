using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110
{
    public class GetInvoiceHeaderParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
