using gestiondette.entities;
using Microsoft.EntityFrameworkCore;

namespace GesDette.Core.Db
{
    public class GesDetteContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Dette> Dette { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Paiement> Paiement { get; set; }

        public DbSet<DetailDette> DetailDette { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Port=5433;Database=gesdettec#;Username=postgres;Password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(u => u.CreateAt).HasColumnName("createat");
            modelBuilder.Entity<User>().Property(u => u.UpdateAt).HasColumnName("updateat");
            modelBuilder.Entity<Client>().ToTable("client");
            modelBuilder.Entity<Client>().Property(c => c.CreateAt).HasColumnName("createat");
            modelBuilder.Entity<Client>().Property(c => c.UpdateAt).HasColumnName("updateat");
            modelBuilder.Entity<Dette>().ToTable("dette");
            modelBuilder.Entity<Article>().ToTable("article");
            modelBuilder.Entity<DetailDette>().ToTable("detaildette");
            modelBuilder.Entity<Paiement>().ToTable("paiement");

        }
    }
}