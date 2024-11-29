using System;
using System.Linq;
using System.Windows.Forms;
using UserInfo;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // 버튼의 Tag에 게임 열거형으로 저장
            play1Button.Tag = UserScore.GameList.JUMP_GAME;
            play2Button.Tag = UserScore.GameList.NUMBER_PUZZLE;
            play3Button.Tag = UserScore.GameList.NUMBER_BASEBALL;
            play4Button.Tag = UserScore.GameList.MEMORY_GAME;
            play5Button.Tag = UserScore.GameList.RANDOM_GAME;
            play6Button.Tag = UserScore.GameList.SNAKE_GAME;
            play7Button.Tag = UserScore.GameList.MINESWEEPER;
            play8Button.Tag = UserScore.GameList.CAR_RACE;
            play9Button.Tag = UserScore.GameList.TETRIS;

            // 모든 버튼의 클릭 이벤트 공통 핸들러로 연결
            var buttons = new[] { play1Button, play2Button, play3Button, play4Button, play5Button, play6Button, play7Button, play8Button, play9Button };
            foreach (var button in buttons)
            {
                button.Click += GamePlayButton_Click;
            }

            Form3 _Form3 = new Form3(this);
            _Form3.ShowDialog();
            if (!m_blLoginCheck) this.Close();
            userIdLabel.Text = UserId;
        }

        private void UpdateScores()
        {
            UserScore user = new UserScore("userScore.txt");
            var scores = new[]
               {
                    game1Score, game2Score, game3Score, game4Score,
                    game5Score, game6Score, game7Score, game8Score
                };

            if (radioButton1.Checked)
            {
                string[] userScore = user.GetLatestScore(UserId);
                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i].Text = userScore[i];
                }
            } 
            else if (radioButton2.Checked)
            {
                string[] userScore = user.GetHighestScore(UserId);
                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i].Text = userScore[i];
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateScores();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateScores();
        }

       /* private void PlayRandomGame()
        {
            Random rnd = new Random();
            var games = Enum.GetValues(typeof(UserScore.GameList));
            UserScore.GameList randomGame = (UserScore.GameList)games.GetValue(rnd.Next(games.Length));

            Form gameForm = new (randomGame);
        }*/
        private void GamePlayButton_Click(object sender, EventArgs e)
        {
            UserScore user = new UserScore("userScore.txt");

            if (sender is Button button)
            {
                if (button.Tag is UserScore.GameList game)
                {
                    // 게임 폼
                    // Form gameForm = null;
                    // 열거형 값 출력 (디버깅용)
                    MessageBox.Show($"You Clicked: {game}");
                    user.SetScore(UserId, game, "0");
                    UpdateScores();
                   
                    // 게임 플레이 및 점수 업데이트 총 구현부

                    /*switch (game)
                    {
                        case UserScore.GameList.JUMP_GAME:
                            gameForm = new GameForm1();
                            break;
                        case UserScore.GameList.NUMBER_PUZZLE:
                            gameForm = new GameForm2();
                            break;
                        case UserScore.GameList.NUMBER_BASEBALL:
                            gameForm = new GameForm3();
                            break;
                        case UserScore.GameList.MEMORY_GAME:
                            gameForm = new GameForm4();
                            break;
                        case UserScore.GameList.RANDOM_GAME:
                            PlayRandomGame();
                            break;
                        case UserScore.GameList.SNAKE_GAME:
                            gameForm = new GameForm6();
                            break;
                        case UserScore.GameList.MINESWEEPER:
                            gameForm = new GameForm7();
                            break;
                        case UserScore.GameList.CAR_RACE:
                            gameForm = new GameForm8();
                            break;
                        case UserScore.GameList.TETRIS:
                            gameForm = new GameForm9();
                            break;
                    }*/

                    /*if (gameForm != null && gameForm.ShowDialog() == DialogResult.OK)
                    {
                        string gameScore = string.Empty;

                        if (gameForm is GameForm1 form1) gameScore = form1.Score;
                        else if (gameForm is GameForm2 form2) gameScore = form2.Score;
                        else if (gameForm is GameForm3 form3) gameScore = form3.Score;
                        else if (gameForm is GameForm4 form4) gameScore = form4.Score;
                        else if (gameForm is GameForm5 form5) gameScore = form5.Score;
                        else if (gameForm is GameForm6 form6) gameScore = form6.Score;
                        else if (gameForm is GameForm7 form7) gameScore = form7.Score;
                        else if (gameForm is GameForm8 form8) gameScore = form8.Score;
                        else if (gameForm is GameForm9 form9) gameScore = form9.Score;
                        // ... 동일하게 처리

                        // 점수 업데이트
                        //user.SetScore(UserId, game, gameScore);

                        // 점수 반영
                        UpdateScores();
                    }*/
                }
            }
        }

        private void scoreInitBtn_Click(object sender, EventArgs e)
        {
            UserScore user = new UserScore("userScore.txt");
            user.InitializeUserScore(UserId);
            UpdateScores();
        }
    }
}
