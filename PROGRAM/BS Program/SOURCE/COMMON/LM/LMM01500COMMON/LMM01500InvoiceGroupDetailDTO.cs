using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public class LMM01500InvoiceGroupDetailDTO
    {

        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }

        public string CINVGRP_NAME { get; set; }
        public string CSEQUENCE { get; set; }
        public bool LACTIVE { get; set; }
        public string CINVOICE_DUE_MODE { get; set; }

        public string CINVOICE_GROUP_MODE { get; set; }
        public int IDUE_DAYS { get; set; }
        public int IFIXED_DUE_DATE { get; set; }
        public int ILIMIT_INVOICE_DATE { get; set; }
        public int IBEFORE_LIMIT_INVOICE_DATE { get; set; }
        public int IAFTER_LIMIT_INVOICE_DATE { get; set; }

        public bool LDUE_DATE_TOLERANCE_HOLIDAY { get; set; }
        public bool LDUE_DATE_TOLERANCE_SATURDAY { get; set; }
        public bool LDUE_DATE_TOLERANCE_SUNDAY { get; set; }

        public bool LUSE_STAMP { get; set; }
        public string CSTAMP_ADD_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CDESCRIPTION { get; set; }

        public bool LBY_DEPARTMENT { get; set; }
        
     //   public string CINVOICE_TEMPLATE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }

        public string CBANK_ACCOUNT { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
      
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        // Data For R_Storage
        public string? CINVOICE_TEMPLATE { get; set; } = "";
        public string? CFileName { get; set; }
        public string? CFileExtension { get; set; }
        public string? CFileNameExtension { get; set; }
        public string CSTORAGE_ID { get; set; } = "";
        public bool LINVOICE_TEMPLATE { get; set; }
        public Byte[]? OData { get; set; }
    }
}
