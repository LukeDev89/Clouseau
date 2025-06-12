using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class HoursPerTaskPerDayModel
    {
        public DateOnly Date { get; set; }
        public decimal Hours { get; set; }
        public decimal ExtraHours { get; set; }
        public string Task { get; set; }
        public string Project { get; set; }
        public string TaskType { get; set; }
        public long UserId { get; set; }
        public string User { get; set; }
    }
}
