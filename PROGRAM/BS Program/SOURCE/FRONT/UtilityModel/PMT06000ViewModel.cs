using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UtilityModel
{
    public class PMT06000ViewModel : R_ViewModel<PMT06000OvtServiceDTO>
    {
        public ObservableCollection<PMT06000OvtServiceGridDTO> loList = new ObservableCollection<PMT06000OvtServiceGridDTO>();
    }
}
