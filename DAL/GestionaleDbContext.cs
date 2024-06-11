using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class GestionaleDbContext(DbContextOptions<GestionaleDbContext> options) : DbContext(options)
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Cliente>(cliente =>
			{
				cliente
					.ToTable("clienti")
					.HasKey(c => c.Id)
					.HasName("pk_clienti");
				cliente
					.Property(c => c.Nome)
					.IsRequired()
					.HasMaxLength(64)
					.HasColumnName("nome");
				cliente
					.Property(c => c.Cognome)
					.IsRequired()
					.HasMaxLength(64)
					.HasColumnName("cognome");
				cliente
					.Property(c => c.Email)
					.HasMaxLength(64)
					.HasColumnName("email");
				cliente
					.Property(c => c.Telefono)
					.HasMaxLength(64)
					.HasColumnName("telefono");
				cliente
					.HasMany(c => c.Prenotazioni)
					.WithOne(p => p.Cliente)
					.HasForeignKey(p => p.IdCliente)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Spettacolo>(spettacolo =>
			{
				spettacolo
					.ToTable("spettacoli")
					.HasKey(t => t.Id)
					.HasName("pk_spettacoli");
				spettacolo
					.Property(t => t.Titolo)
					.IsRequired()
					.HasMaxLength(64)
					.HasColumnName("titolo");
				spettacolo
					.Property(t => t.Descrizione)
					.IsRequired()
					.HasMaxLength(128)
					.HasColumnName("descrizione");
				spettacolo
					.Property(t => t.DataEOra)
					.IsRequired()
					.HasColumnName("data_e_ora");
				spettacolo
					.Property(t => t.Durata)
					.IsRequired()
					.HasColumnName("durata");
				spettacolo
					.Property(t => t.PrezzoBase)
					.IsRequired()
					.HasColumnName("prezzo_base");
				spettacolo
					.HasMany(t => t.Prenotazioni)
					.WithOne(p => p.Spettacolo)
					.HasForeignKey(p => p.IdSpettacolo)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Prenotazione>(prenotazione =>
			{
				prenotazione
					.ToTable("prenotazioni")
					.HasKey(p => p.Id)
					.HasName("pk_prenotazioni");
				prenotazione
					.Property(p => p.IdCliente)
					.IsRequired()
					.HasColumnName("fk_cliente");
				prenotazione
					.Property(p => p.IdSpettacolo)
					.IsRequired()
					.HasColumnName("fk_spettacolo");
				prenotazione
					.Property(p => p.Posto)
					.IsRequired()
					.HasMaxLength(64)
					.HasColumnName("posto");
				prenotazione
					.Property(p => p.Prezzo)
					.IsRequired()
					.HasColumnName("prezzo");
				prenotazione
					.Property(p => p.DataEOraPrenotazione)
					.IsRequired()
					.HasColumnName("data_e_ora_prenotazione");
			});

			/* Nota sulla partecipazione obbligatoria all'associazione tra entità
			A required relationship ensures that every dependent entity must be associated with some principal entity.
			However, a principal entity can always exist without any dependent entities.
			That is, a required relationship does not indicate that there will always be at least one dependent entity.
			*/

		}

		public DbSet<Cliente> Clienti => Set<Cliente>();
		public DbSet<Spettacolo> Spettacoli => Set<Spettacolo>();
		public DbSet<Prenotazione> Prenotazioni => Set<Prenotazione>();
	}
}
