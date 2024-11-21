using System;
using System.Windows.Forms;
using UserInfo;

namespace MainForm
{
    public partial class Form3 : Form
    {
        private Form1 _Form1;
        public Form3(Form1 form1)
        {
            _Form1 = form1;
            InitializeComponent();
        }
        private void BtLogin()
        {
            UserLogin user = new UserLogin("userInfo.txt");
            string id = textBox1.Text;
            string pw = textBox2.Text;

            if (user.Login(id, pw))
            {
                _Form1.LoginCheck = true;
                _Form1.UserId = id;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BtLogin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
