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

        public string SearchUserId(string email, string name)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("모든 항목을 입력해 주세요.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
            else
            {
                try
                {
                    if (!File.Exists(FilePathUser))
                    {
                        MessageBox.Show("유저 파일이 존재하지 않습니다.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // 데이터 베이스의 각각의 유저 데이터를 배열로 저장
                    string[] lines = File.ReadAllLines(FilePathUser);
                    bool is_correct = false;
                    foreach (string line in lines)
                    {
                        string[] userInfo = line.Split(',');
                        if (email.Equals(userInfo[0]) && name.Equals(userInfo[2]))
                        {
                            is_correct = true;
                            return userInfo[3];
                        }
                    }

                    if (!is_correct)
                    {
                        MessageBox.Show("정확한 회원 정보를 입력해 주세요.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"파일 접근 중 오류가 발생했습니다 : {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류가 발생했습니다 : {ex.Message}");
                }
                finally
                {
                    // 자원 회수
                    GC.Collect();
                }
            }
            
            return null;
        }

        public string SearchUserPw(string email, string name, string id)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("모든 항목을 입력해 주세요.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (!File.Exists(FilePathUser))
                    {
                        MessageBox.Show("유저 파일이 존재하지 않습니다.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    bool is_correct = false;
                    string[] lines = File.ReadAllLines(FilePathUser);
                    foreach (string line in lines)
                    {
                        string[] userInfo = line.Split(',');
                        if (email.Equals(userInfo[0]) && name.Equals(userInfo[2]) && id.Equals(userInfo[3]))
                        {
                            is_correct = true;
                            return userInfo[4];
                        }
                    }

                    if (!is_correct)
                    {
                        MessageBox.Show("정확한 회원 정보를 입력해 주세요.", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"파일 접근 중 오류가 발생했습니다 : {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류가 발생했습니다 : {ex.Message}");
                }
                finally
                {
                    GC.Collect();
                }
            }
            return null;
        }
    }
}
