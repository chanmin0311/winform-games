using System;
using System.Windows.Forms;
using UserInfo;
using System.Collections.Generic;

namespace MainForm 
{
    public partial class Form1 : Form
    {

        private bool m_blLoginCheck = false;
        public bool LoginCheck
        {
            get { return m_blLoginCheck; }
            set { m_blLoginCheck = value; }
        }
        public string UserId { get; set; } = "";
        
        public Form1()
        {
            InitializeComponent();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                UserScore userScore = new UserScore("userScore.txt");
                var scores = new[] 
                {
                    game1Score, game2Score,  game3Score,
                    game4Score, game5Score, game6Score, 
                    game7Score, game8Score, game9Score
                };
               
                for (int i = 0 ; i < scores.Length; i++)
                {
                    scores[i].Text = userScore.GetHighestScore(UserId, i + 1);
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ( radioButton2.Checked)
            {
                UserScore userScore = new UserScore("userScore.txt");
                var scores = new[]
                    {
                    game1Score, game2Score,  game3Score,
                    game4Score, game5Score, game6Score,
                    game7Score, game8Score, game9Score
                };

                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i].Text = userScore.GetLatestScore(UserId, i + 1);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form3 _Form3 = new Form3(this);
            _Form3.ShowDialog();
            if (!m_blLoginCheck) this.Close();
            userIdLabel.Text = UserId;
        }

        private void play1Button_Click(object sender, EventArgs e)
        {

            // 게임관련
            /* GameForm1 gameForm1 = new GameForm1();
             gameForm1.ShowDialog();

             string gameScore = gameForm1.Score;*/
            // string gameName = gameForm1.GameName;
            /*UserScore user = new UserScore("userScore.txt");
            user.UpdateScore(UserId, gameName, gameScore);*/
        }
    }
}
