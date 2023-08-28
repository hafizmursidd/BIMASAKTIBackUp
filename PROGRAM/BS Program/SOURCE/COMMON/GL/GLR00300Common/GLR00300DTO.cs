using System;
using R_APICommonDTO;

namespace GLR00300Common
{
    public class GLR00300DTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class GLR00300PeriodDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
    public class GLR00300BudgetNoDTO 
    {
        public string CBUDGET_NO { get; set; }
        public string CBUDGET_NAME_DISPLAY { get; set; }
    }

    //public class GLR00300CurrencyDTO : R_APIResultBaseDTO
    //{
    //    public string CCODE { get; set; }
    //    public string IMAX_YEAR { get; set; }
    //}
}
