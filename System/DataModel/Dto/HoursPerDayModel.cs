using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class HoursPerDayModel
    {
        public DateOnly Date { get; set; }
        public decimal Hours { get; set; }
        public List<string> Tasks { get; set; } = new List<string>();
    }
}
