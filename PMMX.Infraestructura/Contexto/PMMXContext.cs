using PMMX.Modelo.Map;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System.Data.Entity;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Entidades.JustDoIts;

namespace PMMX.Infraestructura.Contexto
{

    /// <summary>
    /// Contexto principal de la aplicacion, Crea el modelo y el Map de las entidades de la aplicacion
    /// </summary>
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PMMXContext : DbContext
    {
        /// <summary>
        /// Contructor del contexto PMMXContext tomando como base PMMXcontext de los settings
        /// </summary>
        public PMMXContext() : base("name=PMMXContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<PMMXContext>(new CreateDatabaseIfNotExists<PMMXContext>());

        }

        /// <summary>
        /// Contructor "Estatico" que retorna la entidad creada sin necesidad de intanciar previamente
        /// </summary>
        /// <returns></returns>
        public static PMMXContext Create()
        {
            return new PMMXContext();
        }

        /// <summary>
        /// Creacion de model dentro de contexto PMMX
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Multimedia

            modelBuilder.Configurations.Add(new FotoMap());

            #endregion

            #region Operaciones

            modelBuilder.Configurations.Add(new WorkCenterMap());
            modelBuilder.Configurations.Add(new ModuloMap());
            modelBuilder.Configurations.Add(new ParoMap());
            modelBuilder.Configurations.Add(new ActividadEnParoMap());
            modelBuilder.Configurations.Add(new DefectoMap());
            modelBuilder.Configurations.Add(new ActividadEnDefectoMap());
            modelBuilder.Configurations.Add(new OrigenMap());
            modelBuilder.Configurations.Add(new BussinesUnitMap());
            modelBuilder.Configurations.Add(new TiempoDeParoMap());
            modelBuilder.Configurations.Add(new ObjetivoVQIMap());
            modelBuilder.Configurations.Add(new DesperdicioMap());
            modelBuilder.Configurations.Add(new MarcaMap());
            modelBuilder.Configurations.Add(new ModuloSeccionMap());
            modelBuilder.Configurations.Add(new NoConformidadMap());


            #endregion

            #region Seguridad

            modelBuilder.Configurations.Add(new PersonaMap());
            modelBuilder.Configurations.Add(new PuestoMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new EntornoMap());
            modelBuilder.Configurations.Add(new DispositivoMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new ShiftLeaderMap());
            modelBuilder.Configurations.Add(new PesadorMap());
            modelBuilder.Configurations.Add(new ElectricosMap());
            modelBuilder.Configurations.Add(new OperadoresMap());
            modelBuilder.Configurations.Add(new HorarioMap());
            modelBuilder.Configurations.Add(new MecanicoMap());

            #endregion

            #region WareHouse



            #endregion



            modelBuilder.Configurations.Add(new PreguntaMap());
            modelBuilder.Configurations.Add(new RespuestaMap());
            modelBuilder.Configurations.Add(new GrupoPreguntasMap());
            modelBuilder.Configurations.Add(new OrigenRespuestaMap());
            modelBuilder.Configurations.Add(new PreguntaTurnoMap());
            modelBuilder.Configurations.Add(new RemitentesMap());
            modelBuilder.Configurations.Add(new ComentarioMap());
            modelBuilder.Configurations.Add(new IndicadoresMap());
            modelBuilder.Configurations.Add(new UsuariosPorPersonaMap());
            modelBuilder.Configurations.Add(new AreaMap());
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new SubCategoriaMap());
            modelBuilder.Configurations.Add(new EventoMap());
            modelBuilder.Configurations.Add(new MantenimientoMap());
            modelBuilder.Configurations.Add(new JustDoItMap());
            modelBuilder.Configurations.Add(new RequisicionDeDescargaMap());
            modelBuilder.Configurations.Add(new ActividadEnVentanaMap());
            modelBuilder.Configurations.Add(new BitacoraVentanaMap());
            modelBuilder.Configurations.Add(new CarrierMap());
            modelBuilder.Configurations.Add(new LocacionMap());
            modelBuilder.Configurations.Add(new ProveedoresMap());
            modelBuilder.Configurations.Add(new RecursosMap());
            modelBuilder.Configurations.Add(new VentanaMap());
            modelBuilder.Configurations.Add(new AsignacionMap());
            modelBuilder.Configurations.Add(new SubAreaMap());
            modelBuilder.Configurations.Add(new ListaDistribucionMap());
            modelBuilder.Configurations.Add(new EventoOrigenMap());
            modelBuilder.Configurations.Add(new EventoResponsableMap());
        }

        #region Multimedia

        public DbSet<Foto> Fotos { get; set; }

        #endregion

        #region Operaciones

        public DbSet<WorkCenter> WorkCenters { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Paro> Paros { get; set; }
        public DbSet<ActividadEnParo> ActividadEnParos { get; set; }
        public DbSet<Defecto> Defectos { get; set; }
        public DbSet<ActividadEnDefecto> ActividadEnDefectos { get; set; }
        public DbSet<Origen> Origens { get; set; }
        public DbSet<BussinesUnit> BussinesUnits { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<NoConformidad> NoConformidades { get; set; }
        public DbSet<TiempoDeParo> TiemposDeParo { get; set; }
        public DbSet<ModuloSeccion> ModuloSeccion { get; set; }
        public DbSet<ObjetivoVQI> ObjetivosVQI { get; set; }
        public DbSet<Desperdicio> Desperdicios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Indicador> Indicadores { get; set; }
        #endregion

        #region Seguridad

        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Entorno> Entornos { get; set; }
        public DbSet<AspNetUser> AspNetUser { get; set; }
        public DbSet<Operadores> Operadores { get; set; }
        public DbSet<Mecanicos> Mecanicos { get; set; }
        public DbSet<UsuariosPorPersona> UsuariosPorPersonas { get; set; }
        public DbSet<ShiftLeaders> ShiftLeaders { get; set; }
        public DbSet<Electricos> Electricos { get; set; }
        public DbSet<Pesador> Pesadores { get; set; }

        #endregion

        #region WareHouse
        #endregion




        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<GrupoPreguntas> GrupoPreguntas { get; set; }
        public DbSet<OrigenRespuesta> OrigenRespuesta { get; set; }
        public DbSet<PreguntaTurno> PreguntaTurno { get; set; }
        public DbSet<Remitentes> Remitentes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<SubCategoria> SubCategoria { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Mantenimiento> Mantenimiento { get; set; }
        public DbSet<JustDoIt> JustDoIt { get; set; }
        public DbSet<RequisicionDeDescarga> RequisicionDeDescargas { get; set; }
        public DbSet<ActividadEnVentana> ActividadEnVentana { get; set; }
        public DbSet<BitacoraVentana> BitacoraVentana { get; set; }
        public DbSet<Carrier> Carrier { get; set; }
        public DbSet<Locacion> Locacion { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Recursos> Recursos { get; set; }
        public DbSet<Ventana> Ventana { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<SubArea> SubArea { get; set; }
        public DbSet<ListaDistribucion> ListaDistribucion { get; set; }
        public DbSet<EventoOrigen> EventoOrigen { get; set; }
        public DbSet<EventoResponsable> EventoResponsable { get; set; }

    }
}

