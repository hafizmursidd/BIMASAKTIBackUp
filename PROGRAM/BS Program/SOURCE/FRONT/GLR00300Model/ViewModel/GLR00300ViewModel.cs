using GLR00300Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00200Common;

namespace GLR00300Model.ViewModel
{
    public class GLR00300ViewModel : R_ViewModel<GLR00300DTO>
    {
        private GLR00300Model _modelGLR00300Model = new GLR00300Model();

        public ObservableCollection<GLR00300DTO> TrialBalanceList =
            new ObservableCollection<GLR00300DTO>();
        public ObservableCollection<GLR00300DTO> PrintMethodList =
            new ObservableCollection<GLR00300DTO>();
        public ObservableCollection<GLR00300BudgetNoDTO> BudgetNoList =
            new ObservableCollection<GLR00300BudgetNoDTO>();

        public GLR00300PeriodDTO PeriodYearMinMax = new GLR00300PeriodDTO();
        public GLR00300DTO CurrencyType = new GLR00300DTO();
        public List<GLR00300DTO> CurrencyTypeList { get; set; } = new List<GLR00300DTO>
            { new GLR00300DTO { CCODE = "L", CDESCRIPTION = "Local Currency" },
              new GLR00300DTO { CCODE = "B", CDESCRIPTION = "Base Currency" } };
        public List<GLR00300DTO> JournalAdustModeList { get; set; } = new List<GLR00300DTO>
        { new GLR00300DTO { CCODE = "S", CDESCRIPTION = "Split" },
            new GLR00300DTO { CCODE = "M", CDESCRIPTION = "Merged" } };

        public GLR00300GetAccountCOA FromAccount = new GLR00300GetAccountCOA();
        public GLR00300GetAccountCOA ToAccount = new GLR00300GetAccountCOA();

        public GLR00300GetAllCenter FromCenter = new GLR00300GetAllCenter();
        public GLR00300GetAllCenter ToCenter = new GLR00300GetAllCenter();
        public List<GetPeriodDTO> GetPeriodList { get; set; }

        //public GLR00300DTO CurrentTrialBalance = new GLR00300DTO()
        //{
        //    CCODE = "N",
        //    CDESCRIPTION = "Normal"
        //};

        public string TrialBalanceTypeValue = "N";
        public string CurrencyTypeValue = "L";
        public string JournalAdjustModeValue = "S";
        public int PeriodYear = DateTime.Now.Year;
        public string PeriodId = "01";
        public bool _lPrintByCenter = false;
        public string PrintMethodValue = "00";
        public bool _lPrintBudget = false;
        public string BudgetNoValue = "";
        public bool _lIsTrialBalanceTypeIsNormal = true;

        public async Task GetTrialBalance()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetTrialBalanceTypeAsyncModel();
                TrialBalanceList = new ObservableCollection<GLR00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYear()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetPeriodAsyncModel();
                PeriodYearMinMax = loResult;
                await GetPeriod();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public async Task GetPeriod()
        {
            GetPeriodList = new List<GetPeriodDTO>();

            for (int i = 1; i <= 15; i++)
            {
                string IdTemp = i.ToString("D2");
                GetPeriodDTO IdPeriod = new GetPeriodDTO { Id = IdTemp };
                GetPeriodList.Add(IdPeriod);
            }

        }
        public async Task GetPrintMethod()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGLR00300Model.GetPrintMethodTypeAsyncModel();
                PrintMethodList = new ObservableCollection<GLR00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetBudgetNo()
        {
            R_Exception loException = new R_Exception();
            try
            {
                string lcPeriodYear = PeriodYear.ToString();
                var loParam = new GLR00300DBParameterDTO();
                loParam.PERIOD_YEAR = lcPeriodYear;
                loParam.CURRENCY_TYPE = CurrencyTypeValue;

                var loResult = await _modelGLR00300Model.GetBudgetNoAsyncModel(loParam);
                BudgetNoList = new ObservableCollection<GLR00300BudgetNoDTO>(loResult.Data);
                //if (BudgetNoList.Count > 0)
                //{
                //    BudgetNoValue = BudgetNoList[0].CBUDGET_NO;
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
