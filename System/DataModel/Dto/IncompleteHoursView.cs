using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class IncompleteHoursView
    {
        public string Integrante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Horas { get; set; }
        public string Estado { get; set; }
    }
}
