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
            else
            {
                // 잘못된 아이디와 비밀번호 입력시 아이디 비번 지움
                if (!string.IsNullOrEmpty(textBox1.Text) || !string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BtLogin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 회원가입 창 열기
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
           if (e.KeyData == Keys.Enter)
            {
                BtLogin();
            }
        }
    }
}
