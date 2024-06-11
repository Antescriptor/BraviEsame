using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DAL.Models
{
	public class Spettacolo(uint id, string titolo, string descrizione, DateTime dataEOra, uint durata, decimal prezzoBase)
	{
		public uint Id { get; set; } = id;
		public string Titolo { get; set; } = titolo;
		public string Descrizione { get; set; } = descrizione;
		public DateTime DataEOra { get; set; } = dataEOra;
		public uint Durata { get; set; } = durata;
		public decimal PrezzoBase { get; set; } = prezzoBase;

		[JsonIgnore]
		public virtual ICollection<Prenotazione> Prenotazioni { get; } = new List<Prenotazione>();
	}
}