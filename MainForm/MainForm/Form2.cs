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

        private void DrawRedBox()
        {
            Graphics g = CreateGraphics();

            int x = emailTxtBox.Location.X;
            int y = emailTxtBox.Location.Y;
            int width = emailTxtBox.Width;
            int height = emailTxtBox.Height;
            Point[] pts = {
                new Point(144, 5), new Point(144 + width + 1, 5),
                new Point(144 + width + 1 ,5 + height + 1), new Point(144, 5 + height + 1)
            };
            g.DrawLines(new Pen(Color.DarkRed), pts);
            g.Dispose();
        }
        private  void emailTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (emailTxtBox.Text.Equals("입력해 주세요."))
            {
                DrawRedBox();
                pwcheckTxtBox.ForeColor = System.Drawing.Color.DarkRed;
                emailTxtBox.Text = "";
            }
        }

        private void phoneTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            phoneTxtBox.ForeColor= System.Drawing.Color.DarkRed;
            if (phoneTxtBox.Text.Equals("입력해 주세요."))
            {
                pwcheckTxtBox.ForeColor = System.Drawing.Color.DarkRed;
                phoneTxtBox.Text = "";
            }
        }

        private void nameTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (nameTxtBox.Text.Equals("입력해 주세요."))
            {
                nameTxtBox.Text = "";
            }
        }

        private void idTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (idTxtBox.Text.Equals("입력해 주세요."))
            {
                idTxtBox.Text = "";
            }
        }

        private void pwTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (pwTxtBox.Text.Equals("입력해 주세요."))
            {
                pwTxtBox.Text = "";
            }

        }

        private void pwcheckTxtBox_MouseUp(object sender, MouseEventArgs e)
        {
            pwcheckTxtBox.ForeColor = System.Drawing.Color.DarkRed;
            if (pwcheckTxtBox.Text.Equals("입력해 주세요."))
            {
                pwcheckTxtBox.Text = "";
            }
        }
    }
}
