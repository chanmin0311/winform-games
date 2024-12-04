# **Winform Group Project**

## **프로젝트 개요**
이 프로젝트는 Winform 기반으로 구현되며, 팀원 간 협업을 통해 메인 플랫폼과 여러 게임 폼을 통합합니다. 모든 팀원은 **GitHub와 Git**을 사용하여 코드 변경 사항을 공유하고 통합하며, 효율적인 협업을 목표로 합니다.

---

## **협업 규칙 및 작업 흐름**

### **1. 브랜치 구조**
- **`main`**: 최종 배포 가능한 안정적인 코드가 유지되는 브랜치. 팀장이 관리하며, 모든 테스트가 완료된 코드만 병합됩니다.
- **`develop`**: 팀원들의 작업이 통합되는 개발 브랜치. 모든 Pull Request는 이 브랜치로 병합됩니다.
- **`feature/*`**: 개별 팀원이 작업하는 기능별 브랜치.
  - 예: `feature/game1`, `feature/main-form`.

---

### **2. 작업 단계**

#### **팀원**
1. **`develop` 브랜치 Pull 및 새로운 작업 브랜치 생성**:
   ```
   bash
   
   git checkout develop
   git pull origin develop
   git checkout -b feature/작업내용

2. **코드 정성 및 Commit**
   - Visual Studio에서 코드를 작성한 후 저장.
   - 작업 내용을 커밋:
   ```
   bash
   
   git add .
   // 특정 파일만 선택하고 싶다면 git add 파일명
   git commit -m "작업 내용 요약"

3. **원격 저장소에 Push**
   ```
   bash
   
   git push origin feature/작업내용

4. **Pull Request(PR)생성**
   - GitHub에서 PR을 생성하여 `feature` **브랜치**를 `develop` **브랜치**로 병합 요청.
   - PR 제목 및 설명 작성:
     - 작업 내용, 테스트 방법, 기타 참고 사항 명시.

---

#### **메인 폼 관리자**
1. **PR 리뷰**
   - 팀원이 생성한 PR의 변경 사항 확인.
   - 코드 스타일, 품질, 충돌 여부 점검.
   - 필요한 경우 코멘트를 남겨 팀원에게 수정 요청.
  
2. **PR병합**
   - 문제가 없다면 PR을 승인하고 `develop` **브랜치로 병합.**
   - GitHub에서 **Merge Pull Request** 버튼 클릭
  
3. `develop` **통합 테스트**
   - 로컬에서 `develop` 브랜치의 최신 코드를 Pull
     ```
     bash
     
     git checkout develop
     git pull origin develop
  - 통합 테스트 완료 후 main 브랜치로 병합

4. `main` **브랜치 병합**
   ```
   bash
   
   git checkout main
   git merge develop
   git push origin main

---

### **3. 커밋 메세지 규칙**
- 명확하고 간결하게 작성:
  - 예: `Added login functionality to main form`
  - 예: `Fixed scoring bug in game1`
 
### **4. 브랜치 네이밍 규칙**
- `feature/작업내용` : 새로운 기능 추가.
   - 예: `feature/game1`
- `bugfis/버그내용`: 버그 수정.
   - 예: `bugfix/login-crash`
- `hotfix/긴급수정`: 긴급 배포를 위한 수정.

---

### **Pull Request(PR) 템플릿**
PR 작성 시 아래의 템플릿읠 활용하여 명확한 설명을 추가합니다.
  ```
markdown

  # 작업 내용
  - [여기에 작업한 내용을 간략히 작성]   
  
  # 테스트 방법
  1. [여기에 작업 내용을 테스트하는 방법 작성]
  2. [테스트 시 필요한 조건이나 전제 추가]   
  
  # 기타 참고 사항
  - [필요시 기타 참고 사항 추가]   
  ```
---

## **Git 명령어 요약**
### 브랜치 작업
1. 새 브랜치 생성
   ```
   bash
   
   git chechout -b feature/작업내용 develop
2. 작업 커밋
   ```
   bash
   
   git add .
   git commit -m "작업 내용 요약"
3. 원격 Push
   ```
   bash
   
   git push origin feature/작업내용

--- 

### **PR 병합 및 테스트**
1. `develop` 최신화
   ```
   bash
   
   git checkput develop
   git pull origin develop
2. `main` 브랜치로 병합
   ```
   bash
   
   git checkout main
   git merge develop
   git push origin main

---

## **팀원 역할**
- **게임 담당 팀원**
  - `feafure` 브랜치에서 작업 후 PR생성.
  - 리뷰 피트백 반영 및 수정.
 
- **메인폼 담당자**
    - PR리뷰 및 병합 관리
    - `develop` 브랜치 통합 테스트 후 `main` 병합.


 ---

 ## **리포지토리 연동 단계**

 ### **1 단계: GitHub 리포지토리 클론**
 1. 공유된 **GitHub 리포지토리 URL**을 복사합니다.
 2. 터미널(또는 명령 프롬프트)을 열고 작업한 디렉터리로 이동한 뒤, 다음 명령어 실행
    ```
    bash
    
    git clone https://gitgub.com/사용자명/리포지토리명.git
    ```
    - 이 명령어는 리포지토리를 로컬에 복사합니다.
    - 사용할 URL: https://github.com/chanmin0311/winform-games.git
3. 디렉터리로 이동
   ```
   bash
   cd 리포지토리명
   ```

--- 

### **2단계: 기본 브랜치로 이동**
클론한 리포지토리의 기본 브랜치는 `main`압나다. 협업을 위해 팀원은 `develop`브랜치로 이동해야 합니다.
1. `develop` **브랜치 가져오기**
   ```
   bash
   
   git fetch origin
   git checkout develop
   ```
2. **최신 코드 동기화**
   ```
   bash

   git pull origin develop
   ```

---

### **3단계: 작업 브랜치 생성**
작업할 기능이나 버그 수정에 따라 새 브랜치를 생성합니다.
1. **새 브랜치 생성**
   - 브랜치 이음 규칙을 따릅니다(예: `feature/game1`)
     ```
     bash

     git checkout -b feature/작업내용 develop
     ```

2. **원격 저장소에 새 브랜치 Push**
   ```
   bash

   git push -u origin feature/작업내용
   ```

---

## **작업 흐름 요약**
### **1. 리포지토리 클론**
```
bash

git clone https://github.com/사용자명/리포지토리명.git
cd 리포지토리명
```
### **2. 브랜치 생성**

```
bash

git fetch origin
git checkout develop
git checkout -b feature/작업내용 develop
```
### **3. 작업 및 Push**
1. 코드 수정 후 저장.
2. 변경 사항 추가.
```
bash

git add . 
```

3. 커밋

```
bash

git commit -m "작업 내용 요약"
```
4. Push
```
bash

git push origin feature/작업내용
```

## **작업 시 주의 사항**
1. **항상 최신 ** `develop` **브랜치를 Pull**
  - 다른 팀원의 작업 내용과 충돌을 방지하기 위해 작업 시작 전에 최신 상태로 업데이트.
  ```
bash

git checkout develop
git pull origin develop
```

2. **작업 단위를 작게 나눔**
- 한 번에 너무 많은 기능을 작업하지 말고, 작고 돌립적인 단위로 PR생성.
3. **커밋 메세지 작성**
  - 명확하고 간결하게 작성
  - 예) Fixed login vaildation logic
 
---

## 팀원의 PR 이후 병합 과정
### 메인폼 관리자가 해야할 일
1. 팀원의 PR을 확인.
2. 코드 리뷰 및 피드백 작성.
3. 승인된 PR을 `develop` 브랜치로 병합.

### 병합 후 팀원이 해야 할 일
1. `develop` **브랜치 최신화**
   ```
   bash

   git checkout develop
   git pull origin develop
   ```

2. **새로운 작업 브랜치 생성**
   ```
   bash

   git checkout -b featu/새작업 develop
   ```
   #### 새로운 브랜치를 생성해야 하는 이유
   - 예상치 못한 문제 발생을 피하기 위해 새로운 작업 브랜치를 생성합니다.
   - **기존 브랜치에서 작업을 계속 Push**하면, 이전 작업과 새 작입이 같은 브랜치에 섞이게 됩니다.
   - 병합된 내용과 새 작업이 섞여 PR을 만들 때, 이 전 커밋까지 다시 포함되어 팀원들에게 혼란을 줄 수 있습니다.
   - 기존 브랜치에서 계속 작업하면 **이전에 병합된 작업이 새로운 PR에 다시 포함**됩니다.
   - 리뷰 시 팀원이 이전에 이미 본 내용까지 다시 리뷰해야 하는 불필요한 작업이 발생합니다.
   - 충돌 가능성이 증가합니다.
  
   #### **기존 브랜치를 실수로 계속 사용했다면?**
   ##### **1. 기존 브랜치를 새로운 브랜치로 복사**
   - 기존 브랜치에서 이미 작업을 시작했더라도, 새로운 브랜치를 만들어 정리할 수 있습니다.
     ```
     bash

     git checkout -b feature/new-task
     ```
   ##### **2. 이전 작업 제거 후 새로운 작업 시작**
   - 기존 브랜치를 병합 후 리셋하여 이전 작업 이력을 제거할 수 있습니다.
   ```
   bash

   git checkout feature/game1
   git reset --hard origin/develop
   ```
