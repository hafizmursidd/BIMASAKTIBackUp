using FastReport;
using System.Collections;
namespace DesignFormGL
{
    public partial class GLR00300DesignReport : Form
    {
        private Report _loReport;
        public GLR00300DesignReport()
        {
            InitializeComponent();
        }
        private void GLDesainReport_Load(object sender, EventArgs e)
        {
            _loReport = new Report();
        }

        private void ButtonFormatAClick(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                GLR00300Common.Model.GenerateDataModel.DefaultDataWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void ButtonFormatE_H_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                GLR00300Common.Model.GenerateDataModel.DefaultDataWithHeaderFormat_EtoH()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

    }


}