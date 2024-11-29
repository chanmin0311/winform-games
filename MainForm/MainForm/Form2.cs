using System;
using UserInfo;
using System.Windows.Forms;
using System.Drawing;

namespace MainForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBoxs_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string name = textBox.Name;
                switch (name)
                {
                    case "emailTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                    case "phoneTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                    case "nameTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                    case "idTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                    case "pwTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                    case "pwcheckTxtBox":
                        if (textBox.Text.Equals("입력해 주세요."))
                        {
                            textBox.ForeColor = System.Drawing.Color.Black;
                            textBox.Text = "";
                        }
                        break;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            TextBox[] textBoxes = {emailTxtBox, phoneTxtBox, nameTxtBox, idTxtBox, pwTxtBox, pwcheckTxtBox};
            foreach (TextBox textBox in textBoxes)
            {
                textBox.MouseUp += textBoxs_MouseUp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserRegistration user = new UserRegistration("userInfo.txt", "userScore.txt");
            if (user.RegisterUser
                (
                    emailTxtBox,
                    phoneTxtBox,
                    nameTxtBox,
                    idTxtBox,
                    pwTxtBox,
                    pwcheckTxtBox
                ))
            {
                this.Close();
            }
        }
    }
}
