using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class VentanaView
    {
        public int Id { get; set; }
        public string PO { get; set; }
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

        public EventoView Evento { get; set; }
        public CarrierView Carrier { get; set; }
        public LocacionView Procedencia { get; set; }
        public LocacionView Destino { get; set; }
        public ProveedoresView Proveedor { get; set; }
    }
}
