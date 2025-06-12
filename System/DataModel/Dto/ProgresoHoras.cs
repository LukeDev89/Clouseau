using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class ProgresoHoras
    {
        public string? ContratoCFO { get; set; }
        public string? ContratoCliente { get; set; }
        public int? HorasContrato { get; set; }
        public decimal? HorasImputadasMes { get; set; }
        public decimal? DiferenciaHoras { get; set; }
    }
}
