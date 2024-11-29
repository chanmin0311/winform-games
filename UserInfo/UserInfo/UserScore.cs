using System;
using System.IO;
using System.Windows.Forms;

namespace UserInfo
{
    public class UserScore
    {
        // 파일 정보
        // id, 게임1 최고 점수, 게임1 최근 점수, ... 게임9 최고 점수, 게임9 최근 점수
        // 각 항목은 콤마로 구분, 띄어쓰기 없음
        readonly private string FilePathScore;

        // 게임 리스트 열거형
        public enum GameList
        {
            JUMP_GAME = 1,                  // 점프게임
            NUMBER_PUZZLE,              // 숫자 퍼즐 게임
            NUMBER_BASEBALL,        // 숫자 야구 게임 
            MEMORY_GAME,                // 기억력 게임
            RANDOM_GAME,               // 랜덤게임 
            SNAKE_GAME,                  // 뱀게임 
            MINESWEEPER,               // 지뢰찾기 
            CAR_RACE,                       // 자동차 게임
            TETRIS                              // 테트리스
        };

        public UserScore(string filePathScore)
        {
            FilePathScore = filePathScore;
        }

        // 점수 기록
        public void SetScore(string id, GameList gameName, string score)
        {
            int gameIndex = ((int)gameName);
            if (File.Exists(FilePathScore))
            {
                // 텍스트파일에 모든 텍스트를 읽고 lines문자열 배열에 저장
                // 배열의 첫번째 원소는 텍스트 파일의 첫번째 줄에 대응됨
                // 즉, 파일의 점수 정보가 한 줄씩 배열의 원소로 차례대로 저장됨
                string[] lines = File.ReadAllLines(FilePathScore);
                int targetLine = -1;                                                        // 수정할 줄의 행수를 저장할 변수
                string modifiedScoreInfo = "";

                // 게임당 점수가 최고, 최신 정보를 포함하고 있으므로 id를 제외하면
                // 1번 게임부터 인덱스 1번째 위치부터 18번까지 구성되있음
                // gameIndex * 2를 하면 해당 게임의 최신 정보의 인덱스로 접근이 가능함
                // 최고 게임 점수는 최신 게임 정보의 바로 전 인덱스임
                int highestScoreIndex = (gameIndex * 2) - 1;
                int latesetScoreIndex = (gameIndex * 2);

                // 텍스트파일에서 id가 포함된 줄을 찾고 해당 줄을 수정함
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(id))
                    {
                        targetLine = i;                                         // id가 포함된 줄이 몇번째 줄인지 확인
                        string[] scoreInfo = lines[i].Split(',');      // id가 포함된 줄의 데이터를 ,로 나누어 각각의 항목을 scoreInfo배열에 저장

                        // 최근 점수 반영
                        scoreInfo[latesetScoreIndex] = score;
                        // 매개변수로 전달 받은 점수(최근 게임 점수)가 기존 최고 기록보다 높으면
                        // 전달 받은 점수를 최고 점수에도 저장
                        if (int.Parse(score) > int.Parse(scoreInfo[highestScoreIndex]))
                        {
                            scoreInfo[highestScoreIndex] = score;
                        }

                        // 게임 점수를 수정한 뒤 문자열 배열을 콤마로 구분한 문자열로 변환
                        // 텍스트 파일을 한 줄씩 읽은 정보를 lines에 한 줄 단위로 저장하고 있으므로
                        // 수정한 내용으로 데이터를 다시 저장하려면 string으로 형변환이 필요함
                        modifiedScoreInfo = string.Join(",", scoreInfo);
                        break;
                    }
                }

                if (targetLine != -1)       // 파일 데이터에 접근하고자 하는 id가 존재할 경우에만 
                {
                    // 나머지 정보는 그대로 유지 시킨 상태로 수정이 필요한 줄의 인덱스로 접근 하여 수정된 값을 대입
                    lines[targetLine] = modifiedScoreInfo;
                    // 수정된 데이터를 덮어씀
                    File.WriteAllLines(FilePathScore, lines);
                }
                else                            // 아이디가 조회되지 않은 경우 알람 
                {
                    MessageBox.Show("ID를 찾을 수 없습니다.");
                }
            }
        }

        // 최고 점수 조회
        public string[] GetHighestScore(string id)
        {
            // 8개의 게임에 대한 점수를 저장할 배열 선언
            string[] highestScore = new string[8];

            // 파일이 존재하는 경우에만 
            if (File.Exists(FilePathScore))
            {
                // 모든 점수 데이터를 lines 배열에 저장
                // lines 배열의 각각의 원소는 파일의 한 줄에 해당하는 값들임
                // 즉, 텍스트파일의 한 줄에 대한 데이터 값들로 저장됨
                string[] lines = File.ReadAllLines(FilePathScore);
                foreach (string line in lines)              // lines의 원소들을 차례대로 line에 대입함
                {
                    // 읽어들인 줄에 입력받은 id가 포함되어 있다면
                    // 해당 줄의 데이터가 처리해야할 데이터임
                    if (line.Contains(id))
                    {
                        // 콤마로 구분되어있는 데이터를 콤마의 앞뒤로 분할하여 string형으로 차례대로 배열에 저장
                        string[] scoreInfo = line.Split(',');

                        for (int i = 1; i < scoreInfo.Length; i++)
                        {
                            if (i % 2 == 1)
                            {
                                int newIndex = i / 2;
                                // 가장 높은 점수는 1번 인덱스부터 시작하여 홀수번 인덱스에 저장되므로
                                // 가장 높은 점수가 저장된 부분만 다시 복사하여 그 배열을 리턴
                                highestScore[newIndex] = scoreInfo[i];
                            }
                        }
                        return highestScore;
                    }
                }
            }
            return highestScore;
        }

        // 최근 점수 조회
        public string[] GetLatestScore(string id)
        {
            // 최근 플레이 점수를 저장할 배열 선언
            string[] latestScore = new string[8];

            // 지정된 경로에 파일에 존재하는 경우에만
            if (File.Exists(FilePathScore))
            {
                // GetHighestScore 메소드와 같은 논리임
                // 값에 접근할 인덱스만 다름
                string[] lines = File.ReadAllLines(FilePathScore);
                foreach (string line in lines)
                {
                    if (line.Contains(id))
                    {
                        // 해당 유저의 점수 정보를 저장
                        string[] scoreInfo = line.Split(',');

                        for (int i = 2; i < scoreInfo.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                int newIndex = (i / 2) - 1;
                                // 최근 플레이 점수는 짝수번 인덱스에 저장
                                // 최근 플레이 점수만 새로운 배열에 저장 후 리넡
                                latestScore[newIndex] = scoreInfo[i];
                            }
                        }
                        return latestScore;
                    }
                }
            }
            return latestScore;
        }

        public void InitializeUserScore(string id)
        {
            try
            {
                // 파일이 존재하지 않는 경우 처리
                if (!File.Exists(FilePathScore))
                {
                    MessageBox.Show("점수 파일이 존재하지 않습니다.");
                    return;
                }

                // 파일 데이터 읽기
                string[] lines = File.ReadAllLines(FilePathScore);
                int targetLine = -1;  // 수정할 줄의 행수를 저장할 변수
                string initializedScoreInfo = ""; // 초기화할 점수 저장할 문자열

                // ID가 포함된 줄 찾기
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(id))
                    {
                        targetLine = i; // ID가 포함된 줄 인덱스 저장
                        string[] scoreInfo = lines[i].Split(','); // 데이터 분리

                        // 점수 초기화 (ID 제외)
                        for (int j = 1; j < scoreInfo.Length; j++)
                        {
                            scoreInfo[j] = "0";
                        }

                        // 초기화된 데이터를 문자열로 병합
                        initializedScoreInfo = string.Join(",", scoreInfo);
                        break;
                    }
                }

                // ID가 존재하는 경우
                if (targetLine != -1)
                {
                    // 대상 줄 수정
                    lines[targetLine] = initializedScoreInfo;
                    // 수정된 데이터를 파일에 저장
                    File.WriteAllLines(FilePathScore, lines);
                    MessageBox.Show("점수가 초기화되었습니다.");
                }
                else
                {
                    // ID가 존재하지 않는 경우
                    MessageBox.Show("ID를 찾을 수 없습니다.");
                }
            }
            catch (IOException ex)
            {
                // 파일 입출력 예외 처리
                MessageBox.Show($"파일 접근 중 오류가 발생했습니다: {ex.Message}");
            }
            catch (Exception ex)
            {
                // 일반적인 예외 처리
                MessageBox.Show($"오류가 발생했습니다: {ex.Message}");
            }
            finally
            {
                // 자원 회수
                GC.Collect();
            }
        }
    }
}
