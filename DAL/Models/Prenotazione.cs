using System;
using System.Text.Json.Serialization;

namespace DAL.Models
{
	public class Prenotazione(uint id, uint idCliente, uint idSpettacolo, string posto, decimal prezzo, DateTime dataEOraPrenotazione)
	{
		public uint Id { get; set; } = id;
		public uint IdCliente { get; set; } = idCliente;
		[JsonIgnore]
		public virtual Cliente? Cliente { get; set; }
		public uint IdSpettacolo { get; set; } = idSpettacolo;
		[JsonIgnore]
		public virtual Spettacolo? Spettacolo { get; set; }
		public string Posto { get; set; } = posto;
		public decimal Prezzo { get; set; } = prezzo;
		public DateTime DataEOraPrenotazione { get; set; } = dataEOraPrenotazione;
	}
}
