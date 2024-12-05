using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInfo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace MainForm
{
    public partial class MemoryGame : Form
    {
        private int matchedCardCount = 0; // 맞춘 카드 쌍 수
        private bool isClickBlocked = false; // 카드 클릭을 차단

        Random Location = new Random(); // 카드 위치 랜덤화를 위한 랜덤 객체
        List<Point> points = new List<Point>(); // 카드 위치 리스트
        bool again = false; // 게임 재시작 여부
        PictureBox pendingImage1; // 첫 번째 선택된 카드
        PictureBox pendingImage2; // 두 번째 선택된 카드

        private string gameScore = null;
        public string Score
        {
            get { return gameScore; } set { gameScore = value; } 
        }

        public MemoryGame()
        {
            InitializeComponent(); // 폼 초기화 (버튼, 레이블 등)
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            ScoreCounter.Text = "0"; // 게임 시작 시 점수 초기화
            label1.Text = "5"; // 타이머 라벨 초기화 (5초로 시작)

            // 모든 PictureBox 제어 요소들을 비활성화하고 카드 위치를 리스트에 저장
            foreach (PictureBox picture in tableLayoutPanel1.Controls)
            {
                picture.Enabled = false;
                points.Add(picture.Location); // 카드의 위치를 리스트에 저장
            }

            // 카드 위치 랜덤 배치
            foreach (PictureBox picture in tableLayoutPanel1.Controls)
            {
                int next = Location.Next(points.Count); // 랜덤으로 위치 선택
                Point p = points[next];
                picture.Location = p; // 선택된 위치로 카드 배치
                points.Remove(p); // 사용된 위치 제거
            }

            // 타이머 시작
            timer2.Start(); //게임 타이머 카운트다운 시작
            timer1.Start(); //카드가 뒤집히도록 5초 타이머 시작

            // 카드 이미지 초기화 및 Tag 설정
            Card1.Tag = "Card1"; DupCard1.Tag = "Card1";
            Card2.Tag = "Card2"; DupCard2.Tag = "Card2";
            Card3.Tag = "Card3"; DupCard3.Tag = "Card3";
            Card4.Tag = "Card4"; DupCard4.Tag = "Card4";
            Card5.Tag = "Card5"; DupCard5.Tag = "Card5";
            Card6.Tag = "Card6"; DupCard6.Tag = "Card6";
            Card7.Tag = "Card7"; DupCard7.Tag = "Card7";
            Card8.Tag = "Card8"; DupCard8.Tag = "Card8";
            Card9.Tag = "Card9"; DupCard9.Tag = "Card9";
            Card10.Tag = "Card10"; DupCard10.Tag = "Card10";
            Card11.Tag = "Card11"; DupCard11.Tag = "Card11";
            Card12.Tag = "Card12"; DupCard12.Tag = "Card12";

            // 카드 이미지 설정 (각 카드에 해당하는 이미지 리소스를 할당)
            Card1.Image = Properties.Resources.Card1;
            DupCard1.Image = Properties.Resources.Card1;
            Card2.Image = Properties.Resources.Card2;
            DupCard2.Image = Properties.Resources.Card2;
            Card3.Image = Properties.Resources.Card3;
            DupCard3.Image = Properties.Resources.Card3;
            Card4.Image = Properties.Resources.Card4;
            DupCard4.Image = Properties.Resources.Card4;
            Card5.Image = Properties.Resources.Card5;
            DupCard5.Image = Properties.Resources.Card5;
            Card6.Image = Properties.Resources.Card6;
            DupCard6.Image = Properties.Resources.Card6;
            Card7.Image = Properties.Resources.Card7;
            DupCard7.Image = Properties.Resources.Card7;
            Card8.Image = Properties.Resources.Card8;
            DupCard8.Image = Properties.Resources.Card8;
            Card9.Image = Properties.Resources.Card9;
            DupCard9.Image = Properties.Resources.Card9;
            Card10.Image = Properties.Resources.Card10;
            DupCard10.Image = Properties.Resources.Card10;
            Card11.Image = Properties.Resources.Card11;
            DupCard11.Image = Properties.Resources.Card11;
            Card12.Image = Properties.Resources.Card12;
            DupCard12.Image = Properties.Resources.Card12;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // 타이머 정지
            // 모든 카드를 활성화하고, 커서를 손 모양으로 변경
            foreach (PictureBox picture in tableLayoutPanel1.Controls)
            {
                picture.Enabled = true;
                picture.Cursor = Cursors.Hand;
                picture.Image = Properties.Resources.Cover2; // 카드 커버 이미지 표시
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int timer = Convert.ToInt32(label1.Text); // 타이머 값 가져오기
            timer = timer - 1; // 타이머 1초 감소
            label1.Text = Convert.ToString(timer); // 타이머 라벨 업데이트

            if (timer == 0)
            {
                timer2.Stop(); // 타이머 중지
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            // 틀린 카드를 뒤집기 위한 처리
            pendingImage1.Image = Properties.Resources.Cover2;
            pendingImage2.Image = Properties.Resources.Cover2;

            // 상태 초기화
            pendingImage1 = null;
            pendingImage2 = null;

            isClickBlocked = false; // 클릭 차단 해제

            // timer3 중지
            timer3.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop(); // 타이머 정지
            timer1.Interval = 5000; // 타이머 간격 5초로 설정
            GameWindow_Load(sender, e); // 게임 초기화 메서드 호출
            timer1.Start(); // 타이머 시작
        }

        private void CheckGameEnd()
        {
            // 맞춘 카드 쌍 수가 12개인지 확인 (24개의 카드가 맞춰졌다는 의미)
            if (matchedCardCount == 12)
            {
                // 게임 종료 메시지 표시
                Score = ScoreCounter.Text;
                MessageBox.Show("축하합니다! 모든 카드를 맞추셨습니다. 게임 종료!");

                // 프로그램 종료
                this.Close(); 
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            PictureBox clickedCard = sender as PictureBox; // 클릭된 카드 가져오기
            if (isClickBlocked || clickedCard == null) return; // 클릭 차단 중이면 무시

            // 이미 선택된 카드인지 확인
            if (pendingImage1 == clickedCard || pendingImage2 == clickedCard)
                return;

            // Tag에 저장된 이미지 리소스 이름을 이용해 이미지 가져오기
            string imageName = clickedCard.Tag.ToString();  // 예: "Card1", "Card2" 등
            clickedCard.Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);

            // 첫 번째 카드 선택
            if (pendingImage1 == null)
            {
                pendingImage1 = clickedCard;
            }
            // 두 번째 카드 선택
            else if (pendingImage1 != null && pendingImage2 == null)
            {
                pendingImage2 = clickedCard;
            }

            // 두 카드가 선택되었을 때 비교
            if (pendingImage1 != null && pendingImage2 != null)
            {
                isClickBlocked = true; // 클릭 차단
                if (pendingImage1.Tag == pendingImage2.Tag)  // 카드들이 일치하는지 확인
                {
                    // 맞은 카드 비활성화
                    pendingImage1.Enabled = false;
                    pendingImage2.Enabled = false;

                    // 점수 업데이트
                    ScoreCounter.Text = (int.Parse(ScoreCounter.Text) + 10).ToString();

                    // 맞춘 카드 쌍 수 증가
                    matchedCardCount++;

                    // 선택된 카드들 초기화
                    pendingImage1 = null;
                    pendingImage2 = null;

                    isClickBlocked = false; // 클릭 차단 해제
                }
                else
                {
                    // 점수 감소
                    ScoreCounter.Text = (int.Parse(ScoreCounter.Text) - 10).ToString();

                    // 틀린 카드를 잠시 보여준 후 뒤집기
                    timer3.Start();
                }
                CheckGameEnd();  // 게임 종료 여부 확인
            }
        }
    }
}
