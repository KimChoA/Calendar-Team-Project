<div align="center">

# 📅 Moble Calendar

### C# WinForms 기반 개인 · 공유 일정 통합 관리 프로그램

[![C#](https://img.shields.io/badge/Language-C%23-512BD4?style=flat-square)]()
[![WinForms](https://img.shields.io/badge/GUI-WinForms-0078D4?style=flat-square)]()
[![MySQL](https://img.shields.io/badge/Database-MySQL-4479A1?style=flat-square)]()
[![TCP/IP](https://img.shields.io/badge/Network-TCP%2FIP-orange?style=flat-square)]()
[![REST API](https://img.shields.io/badge/API-Public%20Data-22C55E?style=flat-square)]()
[![Visual Studio](https://img.shields.io/badge/IDE-Visual%20Studio%202022-7c3aed?style=flat-square)]()
[![Platform](https://img.shields.io/badge/Platform-Windows-0ea5e9?style=flat-square)]()
[![Team Project](https://img.shields.io/badge/Project-Team%20Project-f43f5e?style=flat-square)]()

**개인 일정, 스터디 플래너, 시간표, 다이어리, 공유 캘린더를 하나의 프로그램에서 관리할 수 있는**  
**C# WinForms 기반 통합 일정 관리 프로그램**입니다.

</div>

---

## 📌 프로젝트 개요

**Moble Calendar**는 개인 일정과 학습 계획을 관리하고,  
다른 사용자와 일정을 공유할 수 있도록 개발한  
**C# WinForms 기반 개인 · 공유 일정 통합 관리 프로그램**입니다.

기본적인 캘린더 기능뿐만 아니라,

- 개인 일정 등록 · 수정 · 삭제
- 월 · 주 · 일 단위 일정 확인
- 일정 검색 및 카테고리 관리
- 중요도 · 우선순위 관리
- 스터디 플래너
- 시간표
- 다이어리
- D-Day
- 일정 알림
- 공유 캘린더
- 사용자 초대
- 공유 일정 참여자 관리
- TCP/IP 기반 채팅
- 공공데이터 공휴일 API 연동
- 사용자 맞춤 테마 및 글꼴
- Premium 기능

등을 하나의 프로그램 안에 통합했습니다.

---

## 🎯 프로젝트 목표

본 프로젝트의 주요 목표는 다음과 같습니다.

- 개인 일정과 학습 일정을 하나의 프로그램에서 통합 관리
- 월 · 주 · 일 단위의 직관적인 일정 확인
- 일정 · 플래너 · 시간표 · 다이어리 간 기능 연동
- 여러 사용자가 함께 사용할 수 있는 공유 캘린더 구현
- MySQL 기반 사용자 및 일정 데이터 관리
- TCP/IP 기반 채팅 기능 구현
- 공공데이터 API를 활용한 실제 공휴일 정보 제공
- 사용자 맞춤형 테마 및 글꼴 제공
- 기능별 역할 분담 후 하나의 프로그램으로 통합

---

## 💡 프로젝트 배경

기존 일정 관리 프로그램은 단순 일정 확인 기능에 집중되어 있어  
학습 계획, 시간표, 다이어리, 공유 일정 등을 별도로 관리해야 하는 경우가 많습니다.

본 프로젝트에서는 이러한 불편함을 줄이고,

> **일정 관리 → 학습 계획 → 시간표 → 기록 → 일정 공유**

까지 하나의 프로그램 안에서 처리할 수 있는  
**통합 일정 관리 환경**을 구현하고자 했습니다.

또한 개인 일정 관리 기능에서 더 나아가  
공유 캘린더와 TCP/IP 기반 채팅 기능을 추가하여  
사용자 간 일정 공유와 소통이 가능하도록 확장했습니다.

---

## 🧰 개발 환경 / 기술 스택

| 구분 | 사용 기술 |
|---|---|
| **Language** | C# |
| **GUI** | Windows Forms (WinForms) |
| **Database** | MySQL |
| **Network** | TCP/IP |
| **API** | 공공데이터 Open API |
| **Mail** | SMTP |
| **IDE** | Visual Studio 2022 |
| **Platform** | Windows |
| **Version Control** | GitHub |

---


# 👥 팀 구성 및 역할

본 프로젝트는 **5인 팀 프로젝트**로 진행했습니다.

| 팀원 | 역할 | 주요 담당 |
|---|---|---|
| 이해준 | 팀장 | 프로젝트 총괄, MySQL DB, TCP/IP 채팅, 사용자 · 공유 캘린더 관리 |
| **김초아** | **부팀장** | **일정 관리 및 캘린더, 공휴일 API, 스터디 플래너, 공유 일정 참여자 시각화, 테마 · 글꼴 및 UI 개선** |
| 노종현 | 팀원 | 일정 검색 · 삭제, 카테고리, 주 · 월 · 일 보기, 중요도 · 우선순위, 시간표 |
| 장성찬 | 팀원 | 메인 UI/UX, 일정 알림, 스크롤 일정 탐색, 초대 코드 이메일 연동 |
| 정원형 | 팀원 | 메인 UI/UX, 동적 탭, 일기 · D-Day, TCP/IP 채팅, 프리미엄 · 알림 |

---

# ✨ 주요 기능

## 📅 1. 개인 일정 관리

사용자가 날짜별 일정을 등록하고 관리할 수 있는 기본 기능입니다.

- 일정 등록
- 일정 수정
- 일정 삭제
- 일정 검색
- 날짜별 일정 조회
- 중요도 · 우선순위 설정
- 카테고리별 일정 구분
- 일정별 색상 표시

일정 데이터를 날짜와 카테고리를 기준으로 관리하여  
많은 일정도 쉽게 구분할 수 있도록 구성했습니다.

---

## 🗓️ 2. 월 · 주 · 일 캘린더

하나의 일정 데이터를 다양한 방식으로 확인할 수 있도록  
월간 · 주간 · 일간 캘린더를 구성했습니다.

| 보기 | 설명 |
|---|---|
| **월간 보기** | 한 달 전체 일정 확인 |
| **주간 보기** | 선택한 주의 일정 확인 |
| **일간 보기** | 특정 날짜의 상세 일정 확인 |

각 화면에서 동일한 일정 데이터를 사용할 수 있도록 연동했습니다.

---

## 🔍 3. 일정 검색

등록된 일정이 많아졌을 때  
원하는 일정을 빠르게 찾을 수 있도록 검색 기능을 제공합니다.

- 일정 제목 검색
- 날짜 기준 일정 조회
- 검색 결과 화면 제공
- 검색 결과에서 일정 확인

---

## 🏷️ 4. 일정 카테고리

일정을 종류별로 구분할 수 있도록  
사용자 카테고리 기능을 구현했습니다.

- 카테고리 생성
- 카테고리 수정
- 카테고리 삭제
- 카테고리별 색상 지정
- 일정과 카테고리 연결

학교, 약속, 업무 등 서로 다른 일정 유형을  
시각적으로 구분할 수 있도록 구성했습니다.

---

## ⭐ 5. 중요도 · 우선순위

중요한 일정을 별도로 관리할 수 있도록  
중요도와 우선순위 기능을 제공합니다.

- 중요 일정 설정
- 우선순위 지정
- 일정별 중요도 표시

일정이 많아지더라도  
중요한 일정부터 확인할 수 있도록 구성했습니다.

---

## 📚 6. 스터디 플래너

학습 계획과 공부 시간을 날짜별로 관리할 수 있는  
**캘린더 연동형 스터디 플래너**입니다.

### 주요 기능

- 날짜별 학습 계획
- 체크리스트
- 학습 완료 여부 관리
- 공부 시간 기록
- 시간대별 학습 계획
- 과목별 학습 관리
- **오늘의 총 공부시간 자동 계산**
- 캘린더 날짜와 플래너 자동 연동

캘린더에서 날짜를 변경하면  
해당 날짜의 학습 계획을 바로 확인할 수 있습니다.

---

## 🏫 7. 시간표

학교 수업과 같이 반복적으로 발생하는 일정을  
시간표 형태로 관리할 수 있습니다.

- 수업 등록
- 수업 수정
- 수업 삭제
- 요일 및 시간 설정
- 과목별 일정 표시
- 시간표 UI 관리

일반 일정과 별도로  
정기적으로 반복되는 수업 일정을 직관적으로 관리할 수 있습니다.

---

## 📖 8. 다이어리

캘린더 날짜와 연동하여  
사용자의 하루 기록을 저장할 수 있습니다.

- 날짜별 일기 작성
- 일기 수정
- 일기 삭제
- 기존 작성 내용 불러오기
- 캘린더 날짜와 연동

일정뿐만 아니라  
하루의 기록도 하나의 프로그램에서 관리할 수 있습니다.

---

## ⏳ 9. D-Day

시험, 기념일, 중요한 일정 등을  
D-Day 형태로 등록하고 관리할 수 있습니다.

- D-Day 등록
- 날짜 설정
- 남은 날짜 계산
- D-Day 삭제
- 메인 화면 표시

---

## 🔔 10. 일정 알림

등록된 일정 시간에 맞춰  
사용자에게 알림을 제공하는 기능입니다.

- 알림 일정 등록
- 현재 시간 확인
- 일정 시간 비교
- 조건 충족 시 알림 표시

---

## 👥 11. 공유 캘린더

여러 사용자가 하나의 캘린더를 함께 관리할 수 있습니다.

### 주요 기능

- 공유 캘린더 생성
- 공유 캘린더 참가
- 사용자 초대
- 공유 일정 등록
- 공유 일정 수정
- 공유 일정 삭제
- 멤버 관리
- 사용자 권한 관리

개인 일정뿐만 아니라  
팀이나 친구 단위의 일정도 함께 관리할 수 있습니다.

---

## 🎨 12. 공유 일정 참여자 색상 구분

공유 캘린더에서 어떤 사용자가 일정에 참여하는지  
직관적으로 확인할 수 있도록 색상 기반 UI를 구현했습니다.

- 사용자별 고유 Color Index 적용
- 전체 참여자 선택
- 특정 참여자 선택
- 참여자별 Indicator 표시
- 다수 참여 일정의 색상 동시 표시

공유 일정에서 **누가 해당 일정에 참여하는지 한눈에 확인**할 수 있습니다.

---

## 💬 13. TCP/IP 기반 채팅

공유 캘린더 구성원 간 소통을 위해  
TCP/IP 기반 채팅 기능을 구현했습니다.

- TCP/IP 통신
- 메시지 송수신
- 사용자별 메시지 출력
- 공유 일정 관련 소통
- 채팅 내용 확인

공유 일정을 관리하면서  
프로그램 안에서 바로 의견을 주고받을 수 있도록 구성했습니다.

---

## 📧 14. 이메일 초대

공유 캘린더에 새로운 사용자를 초대할 수 있도록  
이메일 기반 초대 기능을 제공합니다.

```text
공유 캘린더 생성
        ↓
사용자 초대
        ↓
초대 코드 생성
        ↓
이메일 전송
        ↓
사용자 초대 코드 확인
        ↓
공유 캘린더 참가
```

---

## 🔴 15. 공휴일 Open API

공공데이터포털의 공휴일 Open API를 이용하여  
대한민국 공휴일 정보를 자동으로 표시합니다.

- Open API 요청
- 응답 데이터 파싱
- 공휴일명 및 날짜 추출
- 캘린더 날짜 매칭
- 월 · 주 · 일 화면 공통 표시

사용자가 공휴일 정보를 직접 등록하지 않아도  
자동으로 캘린더에 표시할 수 있도록 구현했습니다.

---

## 🎨 16. 사용자 맞춤 테마

사용자가 원하는 분위기에 맞게  
프로그램의 화면 테마를 변경할 수 있습니다.

지원 예시:

- Light
- Dark
- Blossom
- Mint
- Lavender
- Cozy

사용자가 선택한 설정을 저장하여  
재실행 후에도 동일한 환경을 유지할 수 있도록 했습니다.

---

## 🔤 17. 글꼴 변경

사용자가 프로그램 전체 글꼴을 변경할 수 있도록  
글꼴 설정 기능을 구현했습니다.

- 프로그램 전체 글꼴 변경
- 다양한 한글 글꼴 지원
- 선택한 글꼴 설정 저장
- 캘린더 레이아웃과 글꼴 설정 분리

글꼴 변경 시에도  
캘린더 레이아웃이 크게 변형되지 않도록 개선했습니다.

---

## 💎 18. Premium 기능

일반 사용자와 Premium 사용자를 구분하여  
추가 기능 및 UI를 제공하도록 구현했습니다.

- Premium 상태 관리
- Premium 관련 UI
- 사용자별 Premium 정보 저장
- 추가 사용자 설정

---

# 🔄 전체 시스템 흐름

```text
회원가입 / 로그인
        ↓
사용자 정보 및 일정 데이터 불러오기
        ↓
메인 화면
        │
        ├── 개인 캘린더
        │      ├── 일정 등록 / 수정 / 삭제
        │      ├── 월 / 주 / 일 보기
        │      ├── 카테고리 / 중요도
        │      ├── 일정 검색
        │      └── 공휴일 API
        │
        ├── 스터디 플래너
        │      ├── 학습 계획
        │      ├── 체크리스트
        │      └── 공부시간 계산
        │
        ├── 시간표
        │      └── 수업 일정 관리
        │
        ├── 다이어리
        │      └── 날짜별 기록
        │
        ├── 공유 캘린더
        │      ├── 생성 / 참가
        │      ├── 사용자 초대
        │      ├── 공유 일정
        │      ├── 참여자 색상 구분
        │      └── TCP/IP 채팅
        │
        ├── D-Day / 일정 알림
        │
        └── 사용자 설정
               ├── 테마
               ├── 글꼴
               └── Premium
```
---

# 💻 주요 담당 코드

### `CalendarControl.cs`

- **일정 및 캘린더 화면 관리**
- **날짜 선택 및 화면 갱신**
- **월 · 주 · 일 일정 표시**
- **일정 및 공휴일 렌더링**

### `PlannerControl.cs`

- **스터디 플래너**
- **학습 체크리스트**
- **공부시간 관리**
- **총 공부시간 계산**
- **캘린더 날짜 연동**

### `Services/HolidayService.cs`

- **공공데이터 공휴일 Open API 요청**
- **공휴일 데이터 파싱**
- **공휴일 정보 관리**

### `Services/SharedCalendarColorService.cs`

- **공유 캘린더 사용자별 색상 관리**
- **Color Index 처리**

### `SharedCalendarScheduleEditorDialog.cs`

- **공유 일정 등록 / 수정**
- **일정 참여자 선택**
- **참여 사용자 관리**

### `Services/UiThemeService.cs`

- **사용자 테마 관리**
- **화면별 테마 적용**

### `AppFontService.cs`

- **프로그램 전체 글꼴 설정**
- **사용자별 글꼴 관리**

### `mainForm.cs`

- **메인 화면 및 전체 탭 관리**
- **캘린더 · 플래너 · 다이어리 · 시간표 통합**
- **테마 및 글꼴 설정**
- **주요 화면 전환**

> 소스 코드 내부의 **`[포트폴리오 담당]`**, **`[담당 기능]`** 주석을 검색하면  
> 담당 구현 위치를 빠르게 확인할 수 있습니다.

---

# 📂 Repository Structure

```text
Calendar-Team-Project/
│
├── README.md
├── .gitignore
│
└── CalendarProject/
    │
    ├── CreateForm/
    │   ├── CalendarScheduleEditorForm.cs
    │   ├── CalenderScheduleListForm.cs
    │   ├── DiaryEntryForm.cs
    │   ├── SearchResultsForm.cs
    │   ├── SharecalendarManager.cs
    │   ├── UserCategoryEditorForm.cs
    │   └── UserCategoryManagerForm.cs
    │
    ├── Models/
    │   ├── CalendarScheduleEntry.cs
    │   ├── ClassSchedule.cs
    │   ├── DiaryEntry.cs
    │   ├── PlannerModels.cs
    │   ├── TabData.cs
    │   └── UserCategory.cs
    │
    ├── Rendering/
    │   └── CalendarMonthCellRenderer.cs
    │
    ├── Repositories/
    │   ├── ScheduleColorRepository.cs
    │   └── UserCategoryStore.cs
    │
    ├── Services/
    │   ├── CalendarDbRepository.cs
    │   ├── DiaryDbRepository.cs
    │   ├── HolidayService.cs
    │   ├── PlannerDbRepository.cs
    │   ├── SharedCalendarColorService.cs
    │   ├── SharedCalendarRepository.cs
    │   ├── TimetableDbRepository.cs
    │   └── UiThemeService.cs
    │
    ├── CalendarControl.cs
    ├── DiaryControl.cs
    ├── PlannerControl.cs
    ├── Timetable.cs
    ├── SharedCalendarEnter.cs
    ├── SharedCalendarScheduleEditorDialog.cs
    ├── ChatForm.cs
    ├── DdaySettingForm.cs
    ├── AlarmManager.cs
    ├── Login.cs
    ├── Signup.cs
    ├── Mypage.cs
    ├── premium.cs
    ├── mainForm.cs
    ├── Program.cs
    │
    ├── calendar4.csproj
    └── Moble_Team_Project.sln
```

---

# 🔐 공개 저장소 보안

DB 접속 정보, 공공데이터 API Key, 이메일 인증정보는  
GitHub 저장소에 직접 작성하지 않고 **환경 변수로 분리**했습니다.

```text
MOBLE_DB_CONNECTION
MOBLE_HOLIDAY_API_KEY
MOBLE_SMTP_EMAIL
MOBLE_SMTP_APP_PASSWORD
```

민감한 인증정보를 코드와 분리하여  
공개 저장소에서도 안전하게 관리할 수 있도록 구성했습니다.

---

# 🚀 빌드 및 실행

## 1. 개발 환경

- Windows
- Visual Studio 2022
- C# WinForms
- MySQL

## 2. 실행 방법

1. 필요한 환경 변수를 설정합니다.
2. MySQL 데이터베이스 환경을 구성합니다.
3. `Moble_Team_Project.sln`을 Visual Studio에서 엽니다.
4. 필요한 NuGet 패키지를 복원합니다.
5. 프로젝트를 빌드합니다.
6. 프로그램을 실행합니다.

---

# 🎯 핵심 성과

본 프로젝트를 통해 다음 내용을 실제로 구현하고 경험했습니다.

- **C# WinForms 기반 데스크톱 프로그램 개발**
- **개인 일정 관리 시스템 구현**
- **월 · 주 · 일 캘린더 구성**
- **스터디 플래너 및 시간표 구현**
- **공공데이터 Open API 연동**
- **MySQL 기반 사용자 및 일정 데이터 관리**
- **TCP/IP 기반 채팅 시스템**
- **공유 캘린더 및 사용자 관리**
- **공유 일정 참여자별 시각화**
- **사용자 맞춤 테마 및 글꼴**
- **D-Day 및 일정 알림 시스템**
- **팀 단위 기능 개발 및 코드 통합**

특히 **일정 관리, 학습 계획, 기록, 공유 기능을 하나의 프로그램으로 통합**하고,  
사용자 맞춤 설정과 공유 기능까지 확장하여  
실제 일정 관리 프로그램에 가까운 흐름을 구현했다는 점에 의미가 있습니다.
