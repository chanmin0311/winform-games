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
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/작업내용

2. **코드 정성 및 Commit**
   - Visual Studio에서 코드를 작성한 후 저장.
   - 작업 내용을 커밋:
   ```bash
   git add .
   git commit -m "작업 내용 요약"

3. **원경 저장소에 Push**
   ```bash
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
     ```bash
     git checkout develop
     git pull origin develop
  - 통합 테스트 완료 후 main 브랜치로 병합

4. `main` **브랜치 병합**
   ```bash
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
  ```markdown
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
   ```bash
   git chechout -b feature/작업내용 develop
2. 작업 커밋
   ```bash
   git add .
   git commit -m "작업 내용 요약"
3. 원격 Push
   ```bash
   git push origin feature/작업내용

--- 

### **PR 병합 및 테스트**
1. `develop` 최신화
   ```bash
   git checkput develop
   git pull origin develop
2. `main` 브랜치로 병합
   ```bash
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





   
