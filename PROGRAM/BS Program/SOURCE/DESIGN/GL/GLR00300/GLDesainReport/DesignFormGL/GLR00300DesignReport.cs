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

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();

            loData.Add(GLR00300Common.Model.GenerateDataModel.DefaultData());
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }


        private void GLR00300DesignReport_Load(object sender, EventArgs e)
        {

        }

        //private void GLR00300DesignReport_Load(object sender, EventArgs e)
        //{

    }

    /*
     *         private void LMM01000General_Click(object sender, EventArgs e)
    {
        ArrayList loData = new ArrayList();
        loData.Add(LMM01000COMMON.Models.GenerateDataModel.DefaultDataWithHeader());
        loReport.RegisterData(loData, "ResponseDataModel");
        loReport.Design();
    }
     */

}