using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
	public class APITaConAdd(string titolo, string descrizione, DateTime dataEOra, uint durata, decimal prezzoBase)
	{
		[Required]
		[StringLength(64)]
		public string Titolo { get; } = titolo;

		[Required]
		[StringLength(128)]
		public string Descrizione { get; } = descrizione;

		[Required]
		public DateTime DataEOra { get; } = dataEOra;

		[Required]
		public uint Durata { get; } = durata;

		[Required]
		public decimal PrezzoBase { get; } = prezzoBase;
	}
}