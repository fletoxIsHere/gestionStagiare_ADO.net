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
    public partial class FormFormateur : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");

        public FormFormateur()
        {
            InitializeComponent();
        }

        private void FormFormateur_Load(object sender, EventArgs e)
        {
            RemplireCombo();
            RemplireData();
        }
        public void RemplireCombo()
        {
            DataTable dt = new DataTable();
            string query = "select titre_grp,id_grp from groupe";
            SqlCommand cmd2 = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.HasRows == true)
            {
                dt.Load(dr);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "titre_grp";
                comboBox1.ValueMember = "id_grp";
            }



            cnx.Close();
        }
        public void RemplireData()
        {
            this.dataGridView1.Rows.Clear();
            string query = "select p.Num_prof ,p.Nom_prof , p.pre_prof ,p.tel, g.titre_grp from professeur p, groupe g where p.id_grp = g.id_grp ";
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
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
                    this.dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
                    // textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
                    textBox4.Text = dr[3].ToString();
                    comboBox1.Text = dr[4].ToString();
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
            CRUD("insert into professeur values ('" + textBox1.Text + "','" + textBox2.Text + "','"+textBox3.Text+"','" + comboBox1.SelectedValue + "')");
            RemplireData();
        }

        private void Btn_Supprimer_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("voulez-vous vraiment Suprimer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                CRUD("delete from professeur where Num_prof = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "");
                RemplireData();
            } 
        }

        private void Btn_Modifier_Click(object sender, EventArgs e)
        {
            CRUD("update professeur set Nom_prof  ='" + textBox1.Text + "' , pre_prof  = '" + textBox2.Text + "',Date_prof  = '" + textBox3.Text + "', id_grp = '" + comboBox1.SelectedValue + "' where Num_prof  =" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " ");
            RemplireData();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            rech_navig("select * from professeur where nom_prof  like '%" + textBox4.Text + "%'");
        }

        private void Btn_nouveau_Click(object sender, EventArgs e)
        {
            textBox1.Text  = "";
            textBox2.Text  = "";
            textBox3.Text  = "";
            comboBox1.Text = "";
            RemplireData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
