using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
	public class APIPreConAdd(string titolo, DateTime dataEOraInizio, string posto, uint? idCliente = null, string? nome = null, string? cognome = null, string? email = null, string? telefono = null, uint? postiMassimi = null)
	{
		public uint? PostiMassimi { get; set; } = postiMassimi;


		[Required]
		[StringLength(64)]
		public string Titolo { get; } = titolo;

		[Required]
		public DateTime DataEOraInizio { get; } = dataEOraInizio;

		[Required]
		public string Posto { get; } = posto;

		public uint? IdCliente { get; } = idCliente;

		[StringLength(64)]
		public string? Nome { get; } = nome;

		[StringLength(64)]
		public string? Cognome { get; } = cognome;

		[StringLength(64)]
		[EmailAddress(ErrorMessage = "Indirizzo e-mail invalido")]
		public string? Email { get; } = email;

		[StringLength(64)]
		[Phone(ErrorMessage = "Numero di telefono invalido")]
		public string? Telefono { get; } = telefono;
	}
}
