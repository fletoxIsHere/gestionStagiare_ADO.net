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
    public partial class Form_Absence_Stagiaire : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");

        public Form_Absence_Stagiaire()
        {
            InitializeComponent();
        }

        private void Form_Absence_Stagiaire_Load(object sender, EventArgs e)
        {
            RemplireComboStag();
            RemplireData();
        }
        public void RemplireComboStag()
        {
            DataTable dt = new DataTable();
            string query = "select * from Stagiare";
            SqlCommand cmd2 = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.HasRows == true)
            {
                dt.Load(dr);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Nom_Sta";
                comboBox1.ValueMember = "Num_Sta";
            }



            cnx.Close();
        }


        public void RemplireData()
        {
            this.dataGridView1.Rows.Clear();
            //   string query = "select p.Nom_prof,ab.Date_Abscence,ab.Date_entre,ab.nombre_jour ,ab.motif  from Absence_prfesseur ab , professeur p where ab.num_prof = p.num_prof  ";
            string query = "select *  from Absence_Stagiaire ";
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
              //  MessageBox.Show("pas de lignes trouvé");
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
                    comboBox1.Text = dr[0].ToString();
                    dateTimePicker1.Value = DateTime.Parse(dr[1].ToString());
                    textBox1.Text = dr[2].ToString();
                    break;
                }
            }
            else
            {
                // MessageBox.Show("id non trouver");
            }
            cnx.Close();
        }

        private void Btn_Ajouter_Click(object sender, EventArgs e)
        {
            CRUD("insert into Absence_stagiaire values ('" + comboBox1.SelectedValue + "','" + dateTimePicker1.Value + "','" + textBox1.Text + "')");
            RemplireData();
        }

        private void Btn_Supprimer_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("voulez-vous vraiment Suprimer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                CRUD("delete from Absence_stagiaire where Num_Sta = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "");
                RemplireData();
            }
        }

        private void Btn_Modifier_Click(object sender, EventArgs e)
        {
            CRUD("update Absence_stagiaire set Date_Abscence  ='" + dateTimePicker1.Value + "', motif = '" + textBox1.Text + "' where Num_Sta  =" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " ");
            RemplireData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(this.dataGridView1.CurrentRow.Cells[1].Value.ToString());
            textBox1.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
