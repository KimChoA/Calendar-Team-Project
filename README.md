# Moble Calendar

C# WinForms 기반 개인·공유 일정 관리 팀 프로젝트입니다.
개인 캘린더, 공유 캘린더, 스터디 플래너, 시간표, 다이어리, 공휴일 연동, 테마/글꼴 변경 기능을 포함합니다.

## Tech Stack
- C# / .NET 6 / WinForms
- MySQL
- 공공데이터포털 공휴일 Open API
- SMTP(Gmail) 공유 캘린더 초대 메일

## 실행 전 환경 변수
공개 저장소에 DB 비밀번호, API 키, 메일 앱 비밀번호가 포함되지 않도록 민감 정보는 환경 변수로 분리했습니다.

- `MOBLE_DB_CONNECTION`
- `MOBLE_HOLIDAY_API_KEY`
- `MOBLE_SMTP_EMAIL`
- `MOBLE_SMTP_APP_PASSWORD`

Windows PowerShell 예시:

```powershell
setx MOBLE_DB_CONNECTION "Server=...;Port=3306;Database=...;Uid=...;Pwd=...;"
setx MOBLE_HOLIDAY_API_KEY "YOUR_API_KEY"
setx MOBLE_SMTP_EMAIL "YOUR_EMAIL"
setx MOBLE_SMTP_APP_PASSWORD "YOUR_APP_PASSWORD"
```

환경 변수를 설정한 뒤 Visual Studio에서 `Moble_Team_Project.sln`을 열어 실행합니다.

## 주요 폴더
```text
CreateForm/      일정/다이어리/카테고리 생성·편집 화면
Models/          캘린더·플래너·탭 데이터 모델
Rendering/       월간 캘린더 셀 렌더링
Repositories/    로컬 설정/색상 저장 관련 코드
Services/        DB, 공휴일 API, 공유 캘린더, 테마 등 서비스 로직
```

## 포트폴리오 담당 기능 코드
담당 기능은 소스 내부에 `[포트폴리오 담당]`, `[담당 기능]` 주석으로 표시했습니다.
상세 파일 위치는 `docs/PORTFOLIO_CODE_GUIDE.md`에서 확인할 수 있습니다.
