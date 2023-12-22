using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public class LMM01500InvoiceGroupDTO
    {
        public string CINVGRP_CODE { get; set; }
        public string CINVGRP_NAME { get; set; }
        // public string INVOICE_GROUP => $"{CINVGRP_CODE} - {CINVGRP_NAME}" ;

        public string INVOICE_GROUP
        {
            get => _INVOICE_GROUP;
            set => _INVOICE_GROUP = CINVGRP_CODE + "-" + CINVGRP_NAME ;
        }
        private string _INVOICE_GROUP;
    }

    public class LMM01500InvoiceGroupListDTO : R_APIResultBaseDTO
    {
        public List<LMM01500InvoiceGroupDTO> ListData { get; set; }
    }
}