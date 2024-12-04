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
            play1Button.Tag = UserScore.GameList.RANDOM_GAME;
            play2Button.Tag = UserScore.GameList.JUMP_GAME;
            play3Button.Tag = UserScore.GameList.NUMBER_BASEBALL;
            play4Button.Tag = UserScore.GameList.MEMORY_GAME;
            play5Button.Tag = UserScore.GameList.SNAKE_GAME;
            play6Button.Tag = UserScore.GameList.MINESWEEPER;
            play7Button.Tag = UserScore.GameList.CAR_RACE;
            play8Button.Tag = UserScore.GameList.TETRIS;

            // 모든 버튼의 클릭 이벤트 공통 핸들러로 연결
            var buttons = new[] { play1Button, play2Button, play3Button, play4Button, play5Button, play6Button, play7Button, play8Button };
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
                    game5Score, game6Score, game7Score
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


        private void GamePlayButton_Click(object sender, EventArgs e)
        {
            UserScore userScore = new UserScore("userScore.txt");

            if (sender is Button button)
            {
                if (button.Tag is UserScore.GameList game)
                {
                    if (game == UserScore.GameList.RANDOM_GAME)
                    {
                        // 랜덤 게임 로직
                        var gameList = Enum.GetValues(typeof(UserScore.GameList)) // UserScore.GameList타입을 받아 이 열거형의 모든값을 배열로 반환
                            .Cast<UserScore.GameList>()    // 반환값이 Array타입이고 배열의 모든 값을 UserScore.GameLis타입으로 형변환
                            .Where(g => g != UserScore.GameList.RANDOM_GAME)    // 값이 랜덤게임이 아닌 값만 필터링
                            .ToArray();     // 필터링 된 값들을 배열로 반환

                        Random random = new Random();
                        // 랜덤 게임을 제외한 나머지 게임 상수 값을 랜덤으로 선택하기 위해서 위에서 반환된 배열의 요소를 랜덤으로 선택
                        UserScore.GameList selectedGame = gameList[random.Next(gameList.Length)];

                        DialogResult result = MessageBox.Show($"랜덤으로 게임이 선택됩니다. 플레이하시겠습니까?",
                                                              "랜덤 게임",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            LaunchGame(selectedGame);
                        }
                    }
                    else
                    {
                        // 기존 버튼 처리
                        DialogResult result = MessageBox.Show("플레이 하시겠습니까?", "게임 선택", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            LaunchGame(game);
                        }
                    }
                }
            }
        }

        private void LaunchGame(UserScore.GameList game)
        {
            UserScore userScore = new UserScore("userScore.txt");

            switch (game)
            {
                case UserScore.GameList.JUMP_GAME:
                    JumpGame jumpGame = new JumpGame();
                    jumpGame.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.JUMP_GAME, jumpGame.Score);
                    break;
                case UserScore.GameList.NUMBER_BASEBALL:
                    NumberBaseballGame numberBaseballGame = new NumberBaseballGame();
                    numberBaseballGame.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.NUMBER_BASEBALL, numberBaseballGame.Score);
                    break;
                case UserScore.GameList.SNAKE_GAME:
                    SnakeGame snakeGame = new SnakeGame();
                    snakeGame.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.SNAKE_GAME, snakeGame.Score);
                    break;
                case UserScore.GameList.MEMORY_GAME:
                    MemoryGame memoryGame = new MemoryGame();
                    memoryGame.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.MEMORY_GAME, memoryGame.Score);
                    break;
                case UserScore.GameList.MINESWEEPER:
                    MineSweeper mineSweeper = new MineSweeper();
                    mineSweeper.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.MINESWEEPER, mineSweeper.Score);
                    break;
                case UserScore.GameList.CAR_RACE:
                   CarGame carGame = new CarGame();
                   carGame.ShowDialog();
                   userScore.SetScore(UserId, UserScore.GameList.CAR_RACE, carGame.Score);
                    break;
                case UserScore.GameList.TETRIS:
                    Tetris tetris = new Tetris();
                    tetris.ShowDialog();
                    userScore.SetScore(UserId, UserScore.GameList.TETRIS, tetris.Score);
                    break;
            }

            // 점수 업데이트
            UpdateScores();
        }
        private void scoreInitBtn_Click(object sender, EventArgs e)
        {
            UserScore user = new UserScore("userScore.txt");
            user.InitializeUserScore(UserId);
            UpdateScores();
        }    
    }
}
