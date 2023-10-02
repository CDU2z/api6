using apiAuthores.Entidades;
using Microsoft.EntityFrameworkCore;

namespace apiAuthores
{
    public class AppDBContext : DbContext
    {               
        public AppDBContext ( DbContextOptions options ) : base(options) { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
