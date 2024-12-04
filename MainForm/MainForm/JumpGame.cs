using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class JumpGame : Form
    {
        private string game_score = string.Empty;

        public string Score
        {
            get { return game_score; }
            set { game_score = value; }
        }

        // 게임 상태 관련 변수 선언
        private int gravity = 10;             // 중력 효과
        private int jumpSpeed = 0;            // 점프 속도
        private int characterSpeed = 5;      // 캐릭터 좌우 이동 속도
        private bool isJumping = false;       // 점프 상태
        private bool goLeft, goRight;         // 이동 상태
        //private int score = 0;                // 현재 점수
        private int lives = 3;                // 현재 목숨
        private int platformSpeed = 3;        // 플랫폼이 위로 이동하는 속도
        private int difficultyCounter = 0;    // 난이도 조정 카운터
        private int elapsedTime = 0;          // 경과 시간 (초 단위)

        public JumpGame()
        {
            InitializeComponent();
            InitializeGame();       // 게임 초기화 코드 호출
        }

        // 게임 초기화 메소드
        private void InitializeGame()
        {
            // 타이머 설정
            gameTimer.Interval = 20;  // 타이머 간격을 20ms로 설정 (게임이 부드럽게 움직이도록)
            gameTimer.Tick += new EventHandler(gameTimer_Tick);  // 타이머의 Tick 이벤트에 이벤트 핸들러 연결
            gameTimer.Start();  // 타이머 시작

            // 키보드 이벤트 등록
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);  // 키가 눌릴 때 호출되는 메소드 등록
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);      // 키가 떼어질 때 호출되는 메소드 등록

            // 라벨 초기화
            timeLabel.Text = $"Time: {elapsedTime}";  // 경과 시간 초기화
            livesLabel.Text = $"Lives: {lives}";  // 목숨 초기화

            // 경계선 추가
            Paint += new PaintEventHandler(DrawBoundaries);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // 경과 시간 업데이트 (게임 타이머가 20ms마다 실행되므로 50번마다 1초 증가)
            elapsedTime += 1;
            if (elapsedTime % 50 == 0)
            {
                timeLabel.Text = $"Time: {elapsedTime / 50}";
            }

            // 중력 효과 및 점프 처리
            if (!isJumping)
            {
                character.Top += gravity;  // 캐릭터가 점프 중이 아니라면 중력에 의해 아래로 이동
            }
            else
            {
                character.Top -= jumpSpeed;  // 점프 중이라면 캐릭터가 위로 이동
                jumpSpeed--;  // 점프 속도를 점차 감소
                if (jumpSpeed <= 0)
                {
                    isJumping = false;  // 점프가 끝났음을 알림
                }
            }

            // 캐릭터 좌우 이동 처리
            if (goLeft && character.Left > 0) character.Left -= characterSpeed;
            if (goRight && character.Right < this.ClientSize.Width - character.Width) character.Left += characterSpeed;

            // 플랫폼이 위로 스크롤되도록 처리
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag != null && x.Tag.ToString() == "platform")
                {
                    x.Top -= platformSpeed;  // 플랫폼을 위로 이동

                    // 플랫폼이 화면 밖으로 사라지면 다시 위에서 생성
                    if (x.Top + x.Height < 0)
                    {
                        x.Top = this.ClientSize.Height + 100; // 화면 아래에서 재생성
                        x.Left = new Random().Next(0, this.ClientSize.Width - x.Width); // 새로운 랜덤 위치 설정
                    }

                    // 캐릭터와 플랫폼의 충돌 처리 및 착지 로직
                    if (character.Bounds.IntersectsWith(x.Bounds) && !isJumping && character.Bottom >= x.Top && character.Top < x.Bottom)
                    {
                        jumpSpeed = 15;  // 점프 속도 재설정
                        character.Top = x.Top - character.Height;  // 캐릭터를 플랫폼 위에 위치시킴
                        isJumping = false;  // 착지 상태로 변경
                    }
                }
            }

            // 난이도 증가 (일정 시간마다 플랫폼 속도 증가)
            difficultyCounter++;
            if (difficultyCounter >= 500)  // 약 10초마다 (Interval이 20ms이므로 500 x 20ms = 10000ms)
            {
                platformSpeed++;  // 플랫폼 속도 증가로 난이도 상승
                difficultyCounter = 0;  // 카운터 초기화
            }

            // 게임 종료 조건 (캐릭터가 화면 밖으로 떨어질 경우)
            if (character.Top > this.ClientSize.Height || character.Top < -character.Height || character.Left < 0 || character.Right > this.ClientSize.Width)
            {
                lives--;
                livesLabel.Text = $"Lives: {lives}";
                if (lives == 0)
                {
                    GameOver();
                }
                else
                {
                    ResetCharacter();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = true;           // 왼쪽 키가 눌리면 왼쪽으로 이동
            if (e.KeyCode == Keys.Right) goRight = true;         // 오른쪽 키가 눌리면 오른쪽으로 이동
            if (e.KeyCode == Keys.Space && !isJumping)           // 스페이스바가 눌리고 점프 상태가 아닐 때 점프 시작
            {
                isJumping = true;
                jumpSpeed = 15;  // 점프 속도 설정
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;          // 왼쪽 키가 떼어지면 왼쪽 이동 중지
            if (e.KeyCode == Keys.Right) goRight = false;        // 오른쪽 키가 떼어지면 오른쪽 이동 중지
        }

        // 게임 종료 메소드
        private void GameOver()
        {
            gameTimer.Stop();  // 타이머를 멈추어 게임 동작을 정지시킴
            Score = (elapsedTime / 50).ToString();
            MessageBox.Show($"Game Over! Your Score: {elapsedTime / 50} seconds", "Game Over");
            this.Close();
        }

        private void DrawBoundaries(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen boundaryPen = new Pen(Color.Red, 2);

            // 위쪽 경계선
            g.DrawLine(boundaryPen, 0, 0, this.ClientSize.Width, 0);
            // 아래쪽 경계선
            g.DrawLine(boundaryPen, 0, this.ClientSize.Height - 1, this.ClientSize.Width, this.ClientSize.Height - 1);
        }

        // 캐릭터 초기 위치로 재설정하는 메소드
        private void ResetCharacter()
        {
            character.Top = 100;  // 캐릭터의 초기 위치 설정
            character.Left = 100;
            jumpSpeed = 0;  // 점프 속도 초기화
            isJumping = false;  // 점프 상태 초기화
        }
    }
}
