using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class VentanaView
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
        [StringLength(20)]
        public string Temperatura { get; set; }

        // Conductor
        [StringLength(250)]
        public string Conductor { get; set; }
        [StringLength(250)]
        public string MovilConductor { get; set; }
        //
        public bool Activo { get; set; }

        //
        public string ProcedenciaNombre { get; set; }
        public string DestinoNombre { get; set; }
        public string ProveedorNombre { get; set; }
        public string SubCategoriaNombre { get; set; }
        public string CarrierNombre { get; set; }
        public string StatusNombre { get; set; }

        #endregion

        #region Navegacion
        public TipoOperacionView TipoOperacion { get; set; }
        public EventoView Evento { get; set; }
        public CarrierView Carrier { get; set; }
        public LocacionView Procedencia { get; set; }
        public LocacionView Destino { get; set; }
        public ProveedoresView Proveedor { get; set; }
        public SubCategoriaView SubCategoria { get; set; }

        public ICollection<StatusVentanaView> StatusVentana { get; set; }
        public ICollection<BitacoraVentanaView> BitacoraVentana { get; set; }
        #endregion 
    }
}
