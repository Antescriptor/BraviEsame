using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
	public class APIPreConGetAvail(string titolo, DateTime dataEOraInizio, uint? postiMassimi = null)
	{

		[Required]
		[StringLength(64)]
		public string Titolo { get; } = titolo;

		[Required]
		public DateTime DataEOraInizio { get; } = dataEOraInizio;
		public uint? PostiMassimi { get; set; } = postiMassimi;
	}
}
