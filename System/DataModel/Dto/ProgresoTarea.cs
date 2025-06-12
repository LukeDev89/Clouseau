using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Dto
{
    public class ProgresoTarea
    {
        public string? Colaborador { get; set; }
        public string? Proyecto { get; set; }
        public string? Contrato { get; set; }
        public string? Perfil { get; set; }
        public string? Responsable { get; set; }
        public string? RM { get; set; }
        public string? Titulo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateOnly? FechaInicioDate { get => FechaInicio.HasValue ? DateOnly.FromDateTime(FechaInicio.Value) : null; }
        public DateTime? FechaEntrega { get; set; }
        public DateOnly? FechaEntregaDate { get => FechaEntrega.HasValue ? DateOnly.FromDateTime(FechaEntrega.Value) : null; }
        public decimal? TotalHoras { get; set; }
        public decimal? HorasEstimadas { get; set; }
        public decimal? PorcentajeAvance { get; set; }
        public decimal? HorasSemanales { get; set; }
        public decimal? AvanceSemanaPasada { get; set; }
        public decimal? HorasSemanaActual { get; set; }
        public decimal? AvanceSemanaActual { get; set; }
        public decimal? ProyeccionSemanaProxima { get; set; }
    }
}
