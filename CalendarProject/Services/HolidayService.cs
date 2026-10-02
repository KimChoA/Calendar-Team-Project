using System.Globalization;
using System.Xml.Linq;

// ============================================================
// [포트폴리오 담당] 공공데이터포털 공휴일 Open API 연동
// - 연/월 기준 공휴일 데이터 요청
// - XML 응답에서 날짜와 공휴일명을 파싱해 Dictionary로 반환
// ============================================================

namespace calendar4;

public sealed class HolidayService
{
    public async Task<Dictionary<DateTime, string>> GetHolidaysAsync(int year, int month)
    {
        // 공공데이터포털 서비스 키는 환경 변수에서 읽어 GitHub 노출 방지
        var encodedKey = Uri.EscapeDataString(AppSettings.HolidayApiKey);
        var url =
            "https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo" +
            "?serviceKey=" + encodedKey +
            "&solYear=" + year +
            "&solMonth=" + month.ToString("D2") +
            "&pageNo=1&numOfRows=100&_type=xml";

        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        var xml = await client.GetStringAsync(url);
        var document = XDocument.Parse(xml);
        var result = new Dictionary<DateTime, string>();

        foreach (var item in document.Descendants("item"))
        {
            var dateText = item.Element("locdate")?.Value;
            var name = item.Element("dateName")?.Value ?? string.Empty;
            if (DateTime.TryParseExact(
                dateText,
                "yyyyMMdd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
            {
                result[date.Date] = name;
            }
        }

        return result;
    }
}
