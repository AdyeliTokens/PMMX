using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;

namespace PMMX.Modelo.Vistas
{
    public class CRRPorFA
    {
        public string Code_FA { get; set; }
        public List<ReporteTotalView> Daily { get; set; }
        public List<ReporteTotalView> Weekly { get; set; }
        public List<ReporteTotalView> Monthly { get; set; }
        public List<ReporteTotalView> Yearly { get; set; }
    }
}
