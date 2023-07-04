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
    public partial class FormModule : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");

        public FormModule()
        {
            InitializeComponent();
        }

        private void FormModule_Load(object sender, EventArgs e)
        {
            RemplireData();
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
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
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
            CRUD("insert into Module values ('" + textBox2.Text + "','" + textBox3.Text + "')");
            RemplireData();
        }

        private void Btn_Supprimer_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("voulez-vous vraiment Suprimer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                CRUD("delete from Module where Num_Mod = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "");
                RemplireData();
            }
        }

        private void Btn_Modifier_Click(object sender, EventArgs e)
        {
            CRUD("update Module set nom_Mod ='" + textBox2.Text + "' ,coefficient = '" + textBox3.Text + "' where Num_Mod =" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " ");
            RemplireData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            this.textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }

        private void Btn_nouveau_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            RemplireData();
        }
    }
}
