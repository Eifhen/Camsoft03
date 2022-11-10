
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Context
{
    public class BDContext : DbContext {
        public BDContext(DbContextOptions<BDContext> options) : base(options) { }

		public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados {get;set;}
        public DbSet<Nomina> Nomina {get;set;}
        public DbSet<DetalleNomina> DetalleNomina {get;set;}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            
            // primary keys
            modelBuilder.Entity<Departamento>().HasKey( dep => dep.id_dep);
            modelBuilder.Entity<Nomina>().HasKey(n => n.id_nomina);
            modelBuilder.Entity<Empleado>().HasKey(e => e.id);

            // ****************************************
            // ******* Relaciones *********************
            // ****************************************

            // un empleado pertenece a un departamento
            // con muchos empleados
            modelBuilder.Entity<Empleado>()
                        .HasOne(e => e.departamento)
                        .WithMany(d => d.empleados)
                        .HasForeignKey(e => e.id_dep);

            // un detalle de nómina pertenece a una nomina
            // con multiples detalles
            // un detalle de nómina puede contener
            // multiples empleados que contienen multiples 
            // detalles de nómina

            // indica una relación de muchos a muchos agregando 
            // dos relaciones de uno a muchos
            modelBuilder.Entity<DetalleNomina>()  
                        .HasKey(t => new { t.id_emp, t.id_nomina});

            modelBuilder.Entity<DetalleNomina>()
                        .HasOne(n=>n.nomina)
                        .WithMany(d => d.detalle_nomina)
                        .HasForeignKey(d=>d.id_nomina);

            modelBuilder.Entity<DetalleNomina>()
                        .HasOne(e => e.empleado)
                        .WithMany(d => d.detalle_nomina)
                        .HasForeignKey(d => d.id_emp);
                        
		}
   
    }
}