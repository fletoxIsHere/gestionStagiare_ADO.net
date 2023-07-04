using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;


namespace ui_Design
{
    public partial class takerLog : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source=.;Initial Catalog=etudeCas1;Integrated Security=True");


        public takerLog()
        {
            InitializeComponent();
        }

        public void OpenChildForm(Form childForm)
        {
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void takerLog_Load(object sender, EventArgs e)
        {
            this.panelSignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            signup.Visible = false;
            panelSignIn.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            timer2.Start();
            this.panelSignIn.Dock = System.Windows.Forms.DockStyle.Fill;

            signup.Visible = false;
            panelSignIn.Visible = true;
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            timer1.Start();
            this.signup.Dock = System.Windows.Forms.DockStyle.Fill;
            panelSignIn.Visible = false;
            signup.Visible = true;
        }

        private void panelSignIn_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void signup_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void slideimg()
        {
            if (j < 10)
            {
                j++;
                pictureBox3.Dock = DockStyle.Fill;
                pictureBox3.ImageLocation = string.Format(@"C:\Users\Lenovo ThinkPad T570\source\repos\ui_Design\ui_Design\img\{0}.jpg", j);

            }
        }
        int j = 0;

        private void timer1_Tick(object sender, EventArgs e)
        { 
            if (j < 10)
            {
                slideimg();
            }
            else
            {
                timer1.Stop();
                pictureBox3.Visible = false;
                //      btn_signup_Click(sender,e);
                i = 9;

            }

        }
        private void slideimg2()
        {
            if (i < 10)
            {
                i--;
                pictureBox3.Dock = DockStyle.Fill;
                pictureBox3.ImageLocation = string.Format(@"C:\Users\Lenovo ThinkPad T570\source\repos\ui_Design\ui_Design\img\{0}.jpg", i);
            }
        }
        int i = 9;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (i < 10 && 0 < i)
            {
                slideimg2();
            }
            else
            {
                timer2.Stop();
                 j = 0;

                pictureBox3.Visible = false;
                // btn_signup_Click(sender,e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                string query = "select * from login";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cnx.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                bool tr = false;
                while (rd.Read())
                {
                    if (textBox6.Text.Equals(rd[0]) && textBox4.Text.Equals(rd[1]))
                    {
                        tr = true;
                    break;
                    }

                }

                if (tr == true)
                {

                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("mot de passe ou admin incorrect");
                }
                cnx.Close();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                bool IsTrue = false;
              
                if (textBox3.Text == "" || textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Merci de Remplir tous les Champs !!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                
                  
                    IsTrue = true;
                    string query = "INSERT INTO login VALUES ('" + textBox3.Text + "','" + textBox2.Text + "','" + textBox1.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, cnx);
                cnx.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inscription avec Succéss !! ", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2_Click(sender, e);

                if (IsTrue == true)
                    {
                        Application.OpenForms[0].Show();
                    }

                   
                
            }

            catch (Exception)
            {
                MessageBox.Show("Erreur 404 !!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cnx.Close();

        }

        private void panelSignIn_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


