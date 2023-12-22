using System;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public class LMM01500InvoiceGrpDeptDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CBANK_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; }
        public string CINVOICE_TEMPLATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class LMM01500InvoiceGroupDeptListDTO
    {
        public List<LMM01500InvoiceGrpDeptDTO> ListData { get; set; }
    }

    public class LMM01500InvoiceGrpDeptDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }

        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; }
      //s  public string CINVOICE_TEMPLATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        // Data For R_Storage
        public string? CINVOICE_TEMPLATE { get; set; } = "";
        public string? CFileName { get; set; }
        public string? CFileExtension { get; set; }
        public string? CFileNameExtension { get; set; }
        public Byte[]? OData { get; set; }
    }
}