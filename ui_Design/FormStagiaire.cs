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
    public partial class FormStagiaire : Form
    {
         SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");
       
        public FormStagiaire()
        {
            InitializeComponent();
        }

        private void FormStagiaire_Load(object sender, EventArgs e)
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
            //if (dr.HasRows == true)
            //{
            //    while (dr.Read())
            //    {
            //        this.comboBox1.Items.Add(dr[1]);
            //        //this.comboBox1.ValueMember = dr[1].ToString();

            //    }
            //}

            //else
            //{
            //}
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
            string query = "select s.Num_Sta,s.Nom_Sta,pre_Sta,Date_Sta, g.titre_grp from stagiare s, groupe g where s.id_grp = g.id_grp ";
            SqlCommand cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    this.dataGridView1.Rows.Add(dr[0], dr[1], dr[2],dr[3],dr[4]);

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
                    dateTimePicker1.Value = DateTime.Parse(dr[3].ToString());
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

              CRUD("insert into stagiare values ('" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.SelectedValue+"')");
            RemplireData();
        }

        private void Btn_Supprimer_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("voulez-vous vraiment Suprimer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
            CRUD("delete from stagiare where Num_Sta = "+dataGridView1.CurrentRow.Cells[0].Value.ToString()+"");
            RemplireData();
            }
            
        }

        private void Btn_Modifier_Click(object sender, EventArgs e)
        {
            CRUD("update stagiare set Nom_Sta ='"+textBox2.Text+"' , pre_Sta = '"+textBox3.Text+"',Date_Sta = '"+dateTimePicker1.Value+"', id_grp = '"+comboBox1.SelectedValue+"' where Num_Sta ="+dataGridView1.CurrentRow.Cells[0].Value.ToString()+" ");
            RemplireData();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
            rech_navig("select * from stagiare where nom_Sta like '%"+textBox5.Text+ "%'");
        }

        private void Btn_nouveau_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            RemplireData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(this.dataGridView1.CurrentRow.Cells[3].Value.ToString());
            comboBox1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
