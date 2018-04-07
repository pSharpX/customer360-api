using Gildemeister.Cliente360.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gildemeister.Cliente360.Persistence
{
    public class Cliente360DbContext:DbContext
    {
        public Cliente360DbContext(DbContextOptions<Cliente360DbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AplicacionRepositorio> AplicacionRepositorio { get; set; }
        //public virtual DbSet<Asesor> Asesor { get; set; }
        //public virtual DbSet<Cliente> Cliente { get; set; }
        //public virtual DbSet<ClienteContacto> ClienteContacto { get; set; }
        //public virtual DbSet<ClienteVehiculo> ClienteVehiculo { get; set; }
        //public virtual DbSet<Contacto> Contacto { get; set; }
        //public virtual DbSet<Log> Log { get; set; }
        //public virtual DbSet<Logs> Logs { get; set; }
        //public virtual DbSet<Marca> Marca { get; set; }
        //public virtual DbSet<Modelo> Modelo { get; set; }
        //public virtual DbSet<Pais> Pais { get; set; }
        //public virtual DbSet<Persona> Persona { get; set; }
        //public virtual DbSet<PersonaDireccion> PersonaDireccion { get; set; }
        //public virtual DbSet<ProcesoConfiguracion> ProcesoConfiguracion { get; set; }
        //public virtual DbSet<ProcesoConfiguracionDetalle> ProcesoConfiguracionDetalle { get; set; }
        //public virtual DbSet<ProcesoEjecucion> ProcesoEjecucion { get; set; }
        //public virtual DbSet<ProcesoEjecucionDetalle> ProcesoEjecucionDetalle { get; set; }
        //public virtual DbSet<TablaDetalle> TablaDetalle { get; set; }
        //public virtual DbSet<TablaGeneral> TablaGen { get; set; }
        //public virtual DbSet<TipoProceso> TipoProceso { get; set; }
        //public virtual DbSet<Ubigeo> Ubigeo { get; set; }
        //public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<TokenAcceso> TokenAcceso { get; set; }
        public virtual DbSet<ApplicacionLlave> ApplicacionLlave { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
