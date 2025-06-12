using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Request
{
    public class TaskProgressRequest
    {
        public long TaskId { get; set; }
        public long UserId { get; set; }
        public decimal Hours { get; set; }
        public decimal ExtraHours { get; set; }
        public string Comment { get; set; }
        public long TaskTypeId { get; set; }
        public DateTime Date { get; set; }

        public long ProgressId { get; set; }
    }
}
