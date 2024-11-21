using System;
using System.IO;
using System.Windows.Forms;

namespace UserInfo
{
    public class UserLogin
    {
        // 유저 데이터파일 경로
        readonly private string FilePathUser;

        public UserLogin(string filePathUser)
        {
            FilePathUser = filePathUser;
        }

        public bool IsUser(string id, string pw)
        {
            // 파일이 존재하는 경우에만 실행
            if (File.Exists(FilePathUser))
            {
                // 유저 데이터를 읽기 위해 객체 생성
                StreamReader sr = new StreamReader(FilePathUser, System.Text.Encoding.UTF8);
                string line;                                               // 유저 데이터를 저장할 변수
                while ((line = sr.ReadLine()) != null)      // 유저 데이터를 한 줄씩 읽어 텍스트 파일의 처음부터 끝까지 읽음
                {
                    // 유저 데이터 한 줄에 대한 값을 콤마로 분할하여 userInfo배열에 저장
                    string[] userInfo = line.Split(',');
                    // 아이디와 비밀번호를 포함한 유저 데이터 배열의 길이는 4 이상이어야 함
                    // userInfo에 저장된 아이디와 비밀번호가 입력받은 아이디와 비밀번호와 같다면
                    // 이 사용자는 등록된 유저이므로 true 반환
                    if (userInfo.Length > 4 && userInfo[3] == id && userInfo[4] == pw)
                    {
                        return true;
                    }
                }
                sr.Close();
            }
            return false;
        }
        public bool Login(string id, string pw)
        {
            bool loginCheck = false;                        // 로그인 확인 여부를 나타내는 변수

            if (IsUser(id, pw))                                 // 등록된 아이디이고 비밀번호도 일치하면 로그인
            {
                MessageBox.Show("로그인 되었습니다");
                loginCheck = true;
            }
            else                                                       // 등록된 아이디와 비밀번호가 일치하지 않으면 경고 메세지 출력
            {
                MessageBox.Show("올바른 회원정보가 아닙니다.");
                loginCheck = false;
            }
            return loginCheck;
        }
    }
}
