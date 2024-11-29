using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace UserInfo
{
    public class UserRegistration
    {
        // 회원 정보 데이터 파일 경로
        readonly private string FilePathUser;
        // 게임 점수 데이터 파일 경로
        readonly private string FilePathScore;

        public UserRegistration(string filePathUser, string filePathScore)
        {
            FilePathUser = filePathUser;
            FilePathScore = filePathScore;
        }

        // 중복된 아이디 검사
        public bool IsDuplicatedId(string id)            
        {
            // 지정된 경로에 파일이 존재하는 경우에만 실행시킴
            if (File.Exists(FilePathUser))
            {
                StreamReader sr = new StreamReader(FilePathUser, System.Text.Encoding.UTF8);
                string line;                                               // 회원 정보 데이터를 저장할 변수
                while ((line = sr.ReadLine()) != null)      // 회원 데이터를 텍스트파일의 끝까지 한 줄씩 읽음
                {
                    string[] userInfo = line.Split(',');        // 읽어들인 한 줄의 회원정보를 콤마로 구분하여 각 항목을 userInfo에 저장

                    // 회원 정보에서 id는 4번째 순서로 저장되기 때문에 배열의 길이가 일단 3보다 커야하고
                    // 배열의 3번째 인덱스의 원소가 입력받은 id와 같은 경우에는 true(중복됨)을 반환함
                    if (userInfo.Length > 3 && userInfo[3] == id)
                    {
                        return true;
                    }
                }
                sr.Close();                                             // 파일을 다 읽었으므로 닫기
            }
            return false;
        }
        // 회원가입 항목이 다 입력되었는지 검사
        public bool IsAllFilled(TextBox emailTxtBox, TextBox phoneTxtBox, TextBox nameTxtBox, TextBox idTxtBox, TextBox pwTxtBox, TextBox pwCheckTxtBox)
        {
            bool isAllFilled = true;
            var Textboxes = new[] { emailTxtBox, phoneTxtBox, nameTxtBox, idTxtBox, pwTxtBox, pwCheckTxtBox };
            foreach (var TextBox in Textboxes)
            {
                if (string.IsNullOrWhiteSpace(TextBox.Text))
                {
                    TextBox.ForeColor = System.Drawing.Color.DarkRed;
                    TextBox.Text = "입력해 주세요.";
                    isAllFilled = false;
                }
                else
                {
                    TextBox.ForeColor = System.Drawing.Color.Black;
                }
            }
            return isAllFilled;
        }

        // 비밀번호가 확인용 비밀번호와 일치하는지 검사
        public bool IsPasswordCorrect(TextBox pwTxtBox, TextBox pwCheckTxtBox)
        {
            if (pwTxtBox.Text.Equals(pwCheckTxtBox.Text)) return true;
            else return false;
        }

        // 회원가입 진행
        public bool RegisterUser(TextBox emailTxtBox, TextBox phoneTxtBox, TextBox nameTxtBox, TextBox idTxtBox, TextBox pwTxtBox, TextBox pwCheckTxtBox)
        {
            if (!IsAllFilled(emailTxtBox, phoneTxtBox, nameTxtBox, idTxtBox, pwTxtBox, pwCheckTxtBox))
            {
                MessageBox.Show("모든 항목을 입력해 주세요.");
                return false;
            }
            else if (IsDuplicatedId(idTxtBox.Text))                             // 아이디가 중복되는 경우 메세지 박스 출력, 회원가입 진행 불가
            {
                MessageBox.Show("이미 존재하는 아이디 입니다.");
                return false;
            }
            else if (!IsPasswordCorrect(pwTxtBox, pwCheckTxtBox))
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return false;
            }
            else                                                        // 아이디가 중복되지 않았으므로 회원가입을 진행시킴
            {
                // 회원 데이터를 업로드 하기위한 객체 생성
                StreamWriter userStreamWriter = new StreamWriter(FilePathUser, true);
                // 점수 데이터를 업로드 하기위한 객체 생성
                StreamWriter scoreStreamWriter = new StreamWriter(FilePathScore, true);

                // 회원 데이터 쓰기
                userStreamWriter.WriteLine($"{emailTxtBox.Text},{phoneTxtBox.Text},{nameTxtBox.Text},{idTxtBox.Text},{pwCheckTxtBox.Text}");
                // 아이디, 게임 8개에 대한 최대, 최신 점수, 총 17개 의 항목
                // 회원 가입시 사용자 구분을 위한 아이디와 함께 점수 초깃값 0으로 초기화후 데이터 쓰기
                scoreStreamWriter.WriteLine($"{idTxtBox.Text},0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
                MessageBox.Show($"{idTxtBox.Text}님 회원가입이 완료되었습니다!");
                userStreamWriter.Close();
                scoreStreamWriter.Close();
                return true;
            }
        }
    }
}
