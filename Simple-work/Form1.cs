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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        public Form1()
        {
            InitializeComponent();
        }
        public int idp { get; set; }
        public bool isupdatep{ get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = showDataInCombobox();
            comboBox1.DisplayMember = "City";

                
;            if (isupdatep)
            {
                DataTable dtName = ShowInfoMehtod(this.idp);
                DataRow dr = dtName.Rows[0];//dtName.Rows[0] ye lazmi likhn ha isky lye 

                nametextBox1.Text = dr["Name"].ToString();
                comboBox1.Text = dr["City"].ToString();
                pictureBox1.Image = databaseSyPicKrnaImage((byte[])dr["image"]);
                //datarow k obj dr k sath likhna ha isko
            }
        }

        private Image databaseSyPicKrnaImage(byte[] imageV)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageV);
            return Image.FromStream(ms);
        }

       
        private DataTable ShowInfoMehtod(int idp)
        {
            DataTable dt = new DataTable(); //datatabel ka obj khud sy bnana ha lazmi r isko nichy return b krna ha

            SqlCommand cmd = new SqlCommand("fetchById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            //ismy jst @id wala he likhna ha r  koi paramaeters ni lny
            cmd.Parameters.AddWithValue("@id", idp);

            SqlDataReader r = cmd.ExecuteReader();  //SqldataReader ka ak obj ly k isky agy asy he likhna ha
            dt.Load(r);  //datatabe k obj k sath likhna h SqlDataReader h islye ye 
            //datatabke k obj k sath load ho kr braket ma DataRow k obj ko likhna ha 
            conn.Close();
            return dt;
        }

        private void okbutton1_Click(object sender, EventArgs e)
        {
            if (this.isupdatep)
            {
                updateKLye();
            }
            else
            {
                saveKLye();
            }
        }

        private void saveKLye()
        {
            SqlCommand cmd = new SqlCommand("i", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@n", nametextBox1.Text);
            cmd.Parameters.AddWithValue("@city", comboBox1.Text);
            cmd.Parameters.AddWithValue("@image", getPhoto());
            cmd.ExecuteNonQuery();
            MessageBox.Show("successfully enter data");

            DashboardForm d = new DashboardForm();
            d.Show();
        }
        //image k lye
        private byte[] getPhoto()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void updateKLye()
        {
            //SqlCommand cmd = new SqlCommand("up", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //conn.Open();

            //cmd.Parameters.AddWithValue("@n", );
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("successfully update data");
            MessageBox.Show("Update ka code nhe likha");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog()== DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }
        private DataTable showDataInCombobox()
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select City from swtbl", conn);
            adapter.Fill(dt);

            return dt;
        }

    }
}
