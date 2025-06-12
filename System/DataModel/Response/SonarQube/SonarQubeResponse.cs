namespace DataModel.Response.SonarQube
{
    public class SonarQubeResponse
    {
        public string ProjectName { get; set; } = "";
        public SonarQubeComponentModel Metrics { get; set; } = new SonarQubeComponentModel();
    }

    public class SonarQubeComponentModel
    {
        public string Key { get; set; } = "";
        public string Name { get; set; } = "";
        public string Qualifier { get; set; } = "";
        public List<SonarQubeMeasureModel> Measures { get; set; } = new List<SonarQubeMeasureModel>();
    }

    public class SonarQubeMeasureModel
    {
        public string Metric { get; set; } = "";
        public string Value { get; set; } = "";
        public bool BestValue { get; set; }

        public string MetricFormatted { get; set; } = "";
        public string ValueFormatted { get; set; } = "";

        public string Mood { get; set; } = "";
        public string Color { get; set; } = "";
    }
}
