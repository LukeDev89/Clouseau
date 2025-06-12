using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class TimeRecordCsv
    {
        public long ProjectId { get; set; }
        public string Proyecto { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;

        public long UserId { get; set; }
        public string Usuario { get; set; } = "";

        public long TaskTypeId { get; set; }
        public string Actividad { get; set; } = "";

        public long TaskId { get; set; }
        public string RM { get; set; } = "";
        public string Peticion { get; set; } = "";

        public string Comentario { get; set; } = "";
        public decimal Horas { get; set; } = 0;
    }
}
