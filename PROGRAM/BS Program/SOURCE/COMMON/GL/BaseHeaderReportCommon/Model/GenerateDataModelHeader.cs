using System;
using System.Collections.Generic;
using System.Text;
using BaseHeaderReportCommon.BaseHeader;

namespace BaseHeaderReportCommon.Model
{
    public class GenerateDataModelHeader
    {
        public static BaseHeaderResult DefaultData(BaseHeaderDTO poParam)
        {
            BaseHeaderResult loRtn = new BaseHeaderResult();

            loRtn.BaseHeaderData = poParam;


            return loRtn;
        }
    }
}
