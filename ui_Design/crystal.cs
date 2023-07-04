using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ui_Design
{
    public partial class crystal : Form
    {
        public crystal()
        {
            InitializeComponent();
        }
        public string nom2;
        private void crystal_Load(object sender, EventArgs e)
        {

            DataSet1 Ds1 = new DataSet1();
            DataSet1TableAdapters.StagiareTableAdapter ta = new DataSet1TableAdapters.StagiareTableAdapter();
            CrystalReport1 cr1 = new CrystalReport1();
            cr1.SetDataSource(Ds1);

            ta.FillStg(Ds1.Stagiare, nom2);
            this.crystalReportViewer1.ReportSource = cr1;
            this.crystalReportViewer1.Refresh();
        }
    }
}
