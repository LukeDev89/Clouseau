using Newtonsoft.Json;

namespace DataContext.Helper
{
    public class Holiday
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
    }

    public static class DateHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<DateTime>> GetHolidays(int year)
        {
            try
            {
                string url = $"https://nolaborables.com.ar/api/v2/feriados/{year}";
                var response = await client.GetStringAsync(url);
                var holidays = JsonConvert.DeserializeObject<List<Holiday>>(response);
                var holidayDates = new List<DateTime>();
                foreach (var holiday in holidays)
                {
                    holidayDates.Add(new DateTime(year, holiday.Mes, holiday.Dia));
                }
                return holidayDates;
            }
            catch (Exception ex)
            {
                return new List<DateTime>();
            }
            
        }
    }
}
