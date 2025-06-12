using DataModel.Response.SonarQube;
using DataService.Interfaces.SonarQube;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace DataService.Services.SonarQube
{
    public class SonarQubeService : ISonarQubeService
    {
        private readonly string _collectionUri;
        private readonly string _personalAccessToken;

        private const string COLOR_GREEN = "var(--l-green)";
        private const string COLOR_RED = "var(--l-red)";
        private const string MOOD_HAPPY = "sentiment_very_satisfied";
        private const string MOOD_SAD = "sentiment_very_dissatisfied";

        public SonarQubeService(IConfiguration configuration)
        {
            _personalAccessToken = configuration.GetSection("Tokens:SonarQubeToken").Value;
            _collectionUri = configuration.GetSection("SonarQubeConfig:CollectionUri").Value;
        }

        public async Task<List<SonarQubeResponse>> GetSonarQubeMetrics()
        {
            var resposne = new List<SonarQubeResponse>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_personalAccessToken}:")));

                var responseProjects = await client.GetAsync($"{_collectionUri}/projects/search");

                if (responseProjects.IsSuccessStatusCode)
                {
                    var contentProjects = await responseProjects.Content.ReadAsStringAsync();
                    var jsonProjects = JObject.Parse(contentProjects);

                    foreach (var project in jsonProjects["components"])
                    {
                        var projectKey = project["key"].ToString();
                        var projectName = project["name"].ToString();

                        var metrics = "bugs,reliability_rating,violations,vulnerabilities,security_rating,code_smells,sqale_rating,sqale_index,sqale_debt_ratio,coverage,duplicated_blocks,duplicated_lines_density,alert_status";
                        var responseMetrics = await client.GetAsync($"{_collectionUri}/measures/component?component={projectKey}&metricKeys={metrics}");

                        if (responseMetrics.IsSuccessStatusCode)
                        {
                            var contentMetrics = await responseMetrics.Content.ReadAsStringAsync();
                            var measures = JObject.Parse(contentMetrics)["component"]["measures"].ToObject<List<SonarQubeMeasureModel>>().OrderBy(m => m.Metric).ToList();

                            foreach (var m in measures)
                            {
                                m.MetricFormatted = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(m.Metric.Replace('_', ' ')));
                                m.ValueFormatted = m.Value;

                                switch (m.Metric)
                                {
                                    case "alert_status":
                                        m.Color = m.Value == "OK" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "OK" ? MOOD_HAPPY : MOOD_SAD;
                                        m.MetricFormatted = "Quality Status";
                                        m.ValueFormatted = m.Value == "OK" ? "Aprobado" : "Desaprobado";
                                        break;

                                    case "bugs":
                                        m.Color = m.Value == "0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "0" ? MOOD_HAPPY : MOOD_SAD;
                                        break;

                                    case "code_smells":
                                        m.Color = m.Value == "0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "0" ? MOOD_HAPPY : MOOD_SAD;
                                        break;

                                    case "coverage":
                                        m.Color = m.Value == "100" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "100" ? MOOD_HAPPY : MOOD_SAD;
                                        m.ValueFormatted = $"{m.Value} %";
                                        break;

                                    case "duplicated_lines_density":
                                        m.Color = m.Value == "0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "0" ? MOOD_HAPPY : MOOD_SAD;
                                        m.ValueFormatted = $"{m.Value} %";
                                        break;

                                    case "reliability_rating":
                                        m.Color = m.Value == "1.0" || m.Value == "2.0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "1.0" || m.Value == "2.0" ? MOOD_HAPPY : MOOD_SAD;
                                        
                                        switch (m.Value)
                                        {
                                            case "1.0":
                                                m.ValueFormatted = "A";
                                                break;
                                            case "2.0":
                                                m.ValueFormatted = "B";
                                                break;
                                            case "3.0":
                                                m.ValueFormatted = "C";
                                                break;
                                            case "4.0":
                                                m.ValueFormatted = "D";
                                                break;
                                            case "5.0":
                                                m.ValueFormatted = "E";
                                                break;
                                            default:
                                                m.ValueFormatted = "N/A";
                                                break;
                                        }
                                        break;

                                    case "security_rating":
                                        m.Color = m.Value == "1.0" || m.Value == "2.0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "1.0" || m.Value == "2.0" ? MOOD_HAPPY : MOOD_SAD;
                                        
                                        switch (m.Value)
                                        {
                                            case "1.0":
                                                m.ValueFormatted = "A";
                                                break;
                                            case "2.0":
                                                m.ValueFormatted = "B";
                                                break;
                                            case "3.0":
                                                m.ValueFormatted = "C";
                                                break;
                                            case "4.0":
                                                m.ValueFormatted = "D";
                                                break;
                                            case "5.0":
                                                m.ValueFormatted = "E";
                                                break;
                                            default:
                                                m.ValueFormatted = "N/A";
                                                break;
                                        }
                                        break;

                                    case "sqale_rating":
                                        m.Color = m.Value == "1.0" || m.Value == "2.0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "1.0" || m.Value == "2.0"  ? MOOD_HAPPY : MOOD_SAD;

                                        switch (m.Value)
                                        {
                                            case "1.0":
                                                m.ValueFormatted = "A";
                                                break;
                                            case "2.0":
                                                m.ValueFormatted = "B";
                                                break;
                                            case "3.0":
                                                m.ValueFormatted = "C";
                                                break;
                                            case "4.0":
                                                m.ValueFormatted = "D";
                                                break;
                                            case "5.0":
                                                m.ValueFormatted = "E";
                                                break;
                                            default:
                                                m.ValueFormatted = "N/A";
                                                break;
                                        }
                                        break;

                                    case "sqale_index":
                                        m.Color = m.Value == "0" ? COLOR_GREEN : COLOR_RED;
                                        m.Mood = m.Value == "0" ? MOOD_HAPPY : MOOD_SAD;
                                        m.ValueFormatted = (int.Parse(m.Value) / 60 / 8).ToString();
                                        m.MetricFormatted = "Effort Days";
                                        break;

                                    default:
                                        m.Color = COLOR_RED;
                                        m.Mood = MOOD_SAD;
                                        break;
                                }
                            }

                            measures = measures.OrderBy(m => m.MetricFormatted).ToList();

                            resposne.Add(new SonarQubeResponse
                            {
                                ProjectName = projectName,
                                Metrics = new SonarQubeComponentModel
                                {
                                    Key = projectKey,
                                    Name = projectName,
                                    Qualifier = project["qualifier"].ToString(),
                                    Measures = measures
                                }
                            });
                        }
                    }
                }
            }

            return resposne;
        }
    }
}
