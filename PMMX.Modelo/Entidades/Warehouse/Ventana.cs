using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Ventana
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string PO { get; set; }
        public int IdOperacion { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdEvento { get; set; }
        [StringLength(250)]
        public string Recurso { get; set; }
        public double Cantidad { get; set; }
        public int? IdCarrier { get; set; }
        [StringLength(250)]
        public string NombreCarrier { get; set; }
        public int IdProcedencia { get; set; }
        public int IdDestino { get; set; }
        public int IdProveedor { get; set; }
        
        // Vehículo
        [StringLength(250)]
        public string NumeroEconomico { get; set; }
        [StringLength(250)]
        public string NumeroPlaca { get; set; }
        [StringLength(250)]
        public string EconomicoRemolque { get; set; }
        [StringLength(250)]
        public string PlacaRemolque { get; set; }
        [StringLength(250)]
        public string ModeloContenedor { get; set; }
        [StringLength(250)]
        public string ColorContenedor { get; set; }
        [StringLength(250)]
        public string Sellos { get; set; }
        [StringLength(250)]
        public string TipoUnidad { get; set; }
        [StringLength(250)]
        public string Dimension { get; set; }
        public float Temperatura { get; set; }
        
        // Conductor
        [StringLength(250)]
        public string Conductor { get; set; }
        [StringLength(250)]
        public string MovilConductor { get; set; }
        //
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public TipoOperacion TipoOperacion { get; set; }
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
