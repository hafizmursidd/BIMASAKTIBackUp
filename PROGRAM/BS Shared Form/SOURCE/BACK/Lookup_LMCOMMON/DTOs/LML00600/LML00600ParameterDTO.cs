﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_LMCOMMON.DTOs
{
    public class LML00600ParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCUSTOMER_TYPE { get; set; }
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
