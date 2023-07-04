using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ui_Design
{
    public partial class FormNotes : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");

        public FormNotes()
        {
            InitializeComponent();
        }

        private void FormNotes_Load(object sender, EventArgs e)
        {
            RemplireComboStag();
            RemplireComboMod();
        }

        public void RemplireData()
        {
            this.dataGridView1.Rows.Clear();
            string query = "select * from  Module";
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
                }
            }
            else
            {
                MessageBox.Show("pas de lignes trouvé");
            }
            cnx.Close();
        }
        public void CRUD(string query)
        {
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("bien mise a jour");
            cnx.Close();
        }
        public void rech_navig(string query)
        {
            this.dataGridView1.Rows.Clear();
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
                    // textBox1.Text = dr[0].ToString();
                    textBox1.Text = dr[1].ToString();
                    comboBox1.Text = dr[2].ToString();
                    comboBox2.Text = dr[3].ToString();

                    break;
                }
            }
            else
            {
                // MessageBox.Show("id non trouver");
            }
            cnx.Close();
        }
        public void RemplireComboStag()
        {
            string query = "select nom_sta,pre_sta from stagiare ";
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.comboBox1.Items.Add(dr["nom_sta"] + " " + dr["pre_sta"]);
                }
            }
           
            cnx.Close();
        }
        public void RemplireComboMod()
        {
            string query = "Select nom_Mod from Module";
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.comboBox2.Items.Add(dr["nom_Mod"].ToString());
                }
            }
            cnx.Close();
        }

        



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConnectionState.Open == cnx.State)
            {
                cnx.Close();
            }

            cnx.Open();
            string query = "select  m.Num_Mod, m.Nom_Mod , n.Note  from Module m,Notes n, Stagiare s where m.Num_Mod= n.num_Mod and s.Num_Sta= n.Num_Sta and s.nom_sta+' '+s.pre_sta = '" + this.comboBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(query,cnx);
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            this.dataGridView1.DataSource = dt;
            dr.Close();
            string query2 = "select   AVG(n.Note) from Notes n,Stagiare s where n.num_sta = s.num_Sta and s.nom_sta+' '+s.pre_sta ='" + this.comboBox1.Text + "'";
            SqlCommand cmd2 = new SqlCommand(query2, cnx);
            DataTable dt2 = new DataTable();       
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();
                this.textBox2.Text = "" + dr2[0];
            }
            cnx.Close();
       //     label7.Text = "";

        }

        public void evenementCombo27amed()
        {
            string query = "select  m.Num_Mod, m.Nom_Mod ,  n.Note   from Module m,Notes n, Stagiare s where m.Num_Mod= n.num_Mod and s.Num_Sta= n.Num_Sta and s.nom_sta+' '+s.pre_sta = '" + this.comboBox1.Text + "' and m.Nom_Mod = '" + this.comboBox2.Text + "'";
            SqlCommand cmd = new SqlCommand(query, cnx);
            DataTable dt = new DataTable();
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                this.textBox1.Text = "" + dr[2];
            }
            dt.Load(dr);
            this.dataGridView1.DataSource = dt;
            dr.Close();
            cnx.Close();
        }

        public void ajou()
        {
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "PS44";

            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@nomS", SqlDbType.VarChar, 50).Value = this.comboBox1.Text;
            cmd2.Parameters.Add("@nomM", SqlDbType.VarChar, 50).Value = this.comboBox2.Text;
            cmd2.Parameters.Add("@noteF", SqlDbType.Float, 4).Value = float.Parse(this.textBox1.Text);

            cmd2.Connection = cnx;
            cnx.Open();
            cmd2.ExecuteNonQuery();
            MessageBox.Show("insertion bien ajouter");
            cnx.Close();
            
        }
 

        private void Btn_Ajouter_Click(object sender, EventArgs e)
        {

            try
            {
                float note;
                bool fl = float.TryParse(this.textBox1.Text.ToString(), out note);
                if (!fl == true)
                {
                    MessageBox.Show("entrer la note avec une virgule");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "PS44";

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@nomS", SqlDbType.VarChar, 50).Value = this.comboBox1.Text;
                    cmd2.Parameters.Add("@nomM", SqlDbType.VarChar, 50).Value = this.comboBox2.Text;
                    cmd2.Parameters.Add("@noteF", SqlDbType.Float, 4).Value = float.Parse(this.textBox1.Text);

                    cmd2.Connection = cnx;
                    cnx.Open();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("insertion bien ajouter");
                    cnx.Close();
                }


            }
            catch (Exception x)

            {
                MessageBox.Show(x.Message);
            }
            cnx.Close();
            comboBox1_SelectedIndexChanged(sender, e);
           
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            try
            {
             // string query = "delete from Notes where num_sta = (select num_sta from Stagiare s where s.nom_sta+' '+s.pre_sta = '" + this.comboBox1.Text + "') and num_Mod = (select num_Mod from Module where Nom_Mod= '" + this.dataGridView1.CurrentRow.Cells[1].Value.ToString() + "')";
             string query = "delete from notes where num_mod= "+this.dataGridView1.CurrentRow.Cells[0].Value.ToString()+ " and num_Sta = (select num_sta from Stagiare s where s.nom_sta+' '+s.pre_sta = '" + this.comboBox1.Text + "' )";
             SqlCommand cmd = new SqlCommand(query,cnx);
                cnx.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("bien supprimer");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
           
            cnx.Close();
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void btn_Modifier_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd3 = new SqlCommand();
                cmd3.CommandText = "PS22";

                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@nomS", SqlDbType.VarChar, 50).Value = this.comboBox1.Text;
                cmd3.Parameters.Add("@nomM", SqlDbType.VarChar, 50).Value = this.comboBox2.Text;
                cmd3.Parameters.Add("@noteF", SqlDbType.Float, 12).Value = this.textBox1.Text;
                cmd3.Connection = cnx;
                cnx.Open();
                cmd3.ExecuteNonQuery();
                MessageBox.Show("bien modifier");
                cnx.Close();
                comboBox1_SelectedIndexChanged(sender, e);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
           
        }
        crystal c;
        private void button4_Click(object sender, EventArgs e)
        {
            c = new crystal();
            c.nom2 = comboBox1.Text;            
            c.Show();

        }
    }
}
