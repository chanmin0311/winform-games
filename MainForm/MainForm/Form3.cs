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

        private void Form3_Load(object sender, EventArgs e)
        {
            ClientSize = new System.Drawing.Size(ClientSize.Width,  130);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height +140);
                emailLabel.Text = "이메일";
                nameLabel.Text = "이름";
                idLabel.Visible = false;
                idTxtBox.Visible = false;
            }
            else ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height - 140);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height + 140);
                idLabel.Visible = true;
                idTxtBox.Visible = true;
                emailLabel.Text = "이메일";
                nameLabel.Text = "이름";
                idLabel.Text = "아이디";
            }
            else ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height - 140);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin user = new UserLogin("userInfo.txt");
           
            if (radioButton1.Checked == true)
            {
                string id = user.SearchUserId(emailTxtBox.Text, nameTxtBox.Text);
                if (id != null) MessageBox.Show($"아이디 : {id}");
           
            }
            else if (radioButton2.Checked == true)
            {
                string pw = user.SearchUserPw(emailTxtBox.Text, nameTxtBox.Text, idTxtBox.Text);
                if (pw != null) MessageBox.Show($"비밀번호 : {pw}");
            }
            else
            {
                MessageBox.Show("옳바르지 않은 요청입니다.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
