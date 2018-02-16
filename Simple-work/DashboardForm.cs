using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_work
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            
            showData();
        }

        private void showData()
        {
            dataGridView1.DataSource = GetData();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from swtbl", conn);
            adapter.Fill(dt);

            return dt;
        }
        //button tk k lye  yha tk
        private void addbutton1_Click(object sender, EventArgs e)
        {
            //ShowWithProperties(0,false);
            showwithpropertie(0, false);
        }

        private void showwithpropertie(int v1, bool v2)
        {
            Form1 f = new Form1();
            f.idp = v1;
            f.isupdatep = v2;
            f.Show();
        }

        private void ShowWithProperties(int id, bool isupdate)
        {
            //Form1 obj = new Form1();
            //obj.idp = id;
            //obj.isupdatep = isupdate;
            //obj.Show();
        }
        //datagrdview k lye
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //int rowUpdate = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            //int updateObj =Convert.ToInt16( dataGridView1.Rows[rowUpdate].Cells[0].Value);

            //ShowWithProperties(updateObj,true);

            int index = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int row =(int) dataGridView1.Rows[index].Cells[0].Value;
            showwithpropertie(row, true);

            //showData();
        }
    }
}
