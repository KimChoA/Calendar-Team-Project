# 포트폴리오 담당 기능 코드 가이드

포트폴리오 초안의 담당 파트를 실제 코드 파일과 연결해 정리한 문서입니다.

## 1. 공유 캘린더 참여자 식별 및 일정 시각화
- `Services/SharedCalendarRepository.cs`
  - 공유 캘린더 참가 시 사용자별 `ColorIndex` 자동 배정
  - 사용자 ID / 이름 / 색상 인덱스 조회
- `Services/SharedCalendarColorService.cs`
  - `ColorIndex`를 실제 UI 색상으로 변환
  - 테마별 참여자 색상 팔레트 관리
- `SharedCalendarScheduleEditorDialog.cs`
  - 전체 참여 또는 특정 참여자 선택
  - 일정 저장 시 선택된 참여자와 색상 인덱스를 함께 저장
- `CalendarControl.cs`
  - 공유 일정 렌더링 및 테마/글꼴 반영

## 2. 공공데이터 API 연동 및 공휴일 자동 반영
- `Services/HolidayService.cs`
  - 공공데이터포털 공휴일 API 호출
  - XML 응답 파싱 후 날짜별 공휴일 맵 생성
- `mainForm.cs`
  - 월 변경 시 비동기 공휴일 조회
  - 조회 결과를 열린 캘린더 컨트롤에 공통 적용
- `CalendarControl.cs`
  - 월간/주간/일간 화면에 동일 공휴일 데이터 사용
  - 공휴일 날짜 및 제목 표시

## 3. 캘린더 연동형 스터디 플래너
- `PlannerControl.cs`
  - 캘린더에서 선택한 날짜와 플래너 날짜 동기화
  - 날짜별 체크리스트 및 시간표 조회/저장
  - 10분 단위 학습 블록 관리
  - 총 학습시간 자동 계산
- `Services/PlannerDbRepository.cs`
  - 사용자별 플래너 데이터 DB 저장/조회
- `Models/PlannerModels.cs`
  - 체크리스트와 시간 블록 데이터 구조
- `mainForm.cs`
  - 날짜 변경 시 캘린더와 플래너를 동일 날짜 기준으로 갱신

## 4. 사용자 맞춤형 테마 및 UI 개선
- `Services/UiThemeService.cs`
  - Light/Dark 및 추가 테마 색상 정책 관리
  - 하위 컨트롤까지 테마 재귀 적용
- `AppFontService.cs`
  - 글꼴 선택/생성 및 프리미엄 글꼴 구분
  - 미설치 글꼴 사용 시 기본 글꼴 fallback
- `mainForm.cs`
  - 사용자 테마/글꼴 설정 저장 및 전체 탭 반영
- `CalendarControl.cs`, `PlannerControl.cs`
  - 화면별 글꼴/테마 적용 로직 분리
  - 캘린더 셀 크기와 플래너 레이아웃 변형 최소화

## GitHub 공개용 정리 사항
- `.vs/`, `bin/`, `obj/`, `*.user` 등 개발 환경/빌드 산출물 제거
- DB 접속 정보, 공공데이터 API 키, SMTP 인증정보를 `AppSettings.cs`의 환경 변수 방식으로 변경
- 빌드 산출물과 로컬 설정 파일이 다시 올라가지 않도록 `.gitignore` 추가
