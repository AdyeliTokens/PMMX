using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Ventana
    {
        #region Propiedades
        public int Id { get; set; }
        public string PO { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdEvento { get; set; }
        public string Recurso { get; set; }
        public double Cantidad { get; set; }
        public int IdCarrier { get; set; }
        public int IdProcedencia { get; set; }
        public int IdDestino { get; set; }
        public int IdProveedor { get; set; }
        // Vehículo
        public string NumeroEconomico { get; set; }
        public string NumeroPlaca { get; set; }
        public string TipoUnidad { get; set; }
        public string Dimension { get; set; }
        public float Temperatura { get; set; }
        // Conductor
        public string Conductor { get; set; }
        public string MovilConductor { get; set; }
        //
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Evento Evento { get; set; }
        public Carrier Carrier { get; set; }
        public Locacion Procedencia { get; set; }
        public Locacion Destino { get; set; }
        public Proveedores Proveedor { get; set; }
        public SubCategoria SubCategoria { get; set; }

        public ICollection<StatusVentana> StatusVentana { get; set; }
        public ICollection<BitacoraVentana> BitacoraVentana { get; set; }
        #endregion 
    }
}
