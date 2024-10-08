using Microsoft.EntityFrameworkCore;
namespace Domain
{
    public class AuthContext : DbContext
    {
        public AuthContext()
        {
        }

        public AuthContext(DbContextOptions<AuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SegUsuario> SegUsuario { get; set; }
        public virtual DbSet<GdiaPersonal> GdiaPersonal { get; set; }
        public virtual DbSet<GdiaPersonalEspecialidad> GdiaPersonalEspecialidad { get; set; }
        public virtual DbSet<DireccionProvincia> DireccionProvincia { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GdiaPersonal>()
                 .HasMany(p => p.GdiaPersonalEspecialidades)
                 .WithOne(e => e.GdiaPersonal)
                 .HasForeignKey(e => e.GdiaPersonalEspecialidadId);

            modelBuilder.Entity<GdiaPersonalEspecialidad>()
                .HasOne(e => e.GdiaPersonal)
                .WithMany(p => p.GdiaPersonalEspecialidades)
                .HasForeignKey(e => e.GdiaPersonalEspecialidadId);

        }
    }
}