using Microsoft.EntityFrameworkCore;
using biblioteca.Models;

namespace biblioteca.Data
{
    public class bibliotecaContext : DbContext
    {
        public bibliotecaContext(DbContextOptions<bibliotecaContext> options)
            : base(options) { }

        public DbSet<Autores> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 👇 Fuerza el nombre correcto de las tablas en SQL Server
            modelBuilder.Entity<Autores>().ToTable("Autores");
            modelBuilder.Entity<Libros>().ToTable("Libros");

            // 👇 Asegura que las Primary Keys estén definidas
            modelBuilder.Entity<Autores>().HasKey(a => a.AutorID);
            modelBuilder.Entity<Libros>().HasKey(l => l.ID);

            // 👇 Relación 1 a muchos
            modelBuilder.Entity<Libros>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Libros)
                .HasForeignKey(l => l.AutorID);
        }
    }
}
