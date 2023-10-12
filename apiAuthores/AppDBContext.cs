using apiAuthores.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apiAuthores
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

        //esto es si sobrescribimos el model; mantener el base... como primera linea para que funcione identity.
        //protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}