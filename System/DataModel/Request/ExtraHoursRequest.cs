using System;

namespace DataModel.Request
{
    public class ExtraHoursRequest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TaskId { get; set; }
        public long TaskTypeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; } = "";
    }
}