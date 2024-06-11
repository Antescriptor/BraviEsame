using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
	public class APICliConAdd(string nome, string cognome, string? email = null, string? telefono = null)
	{
		[Required]
		[StringLength(64)]
		public string Nome { get; } = nome;
		[Required]
		[StringLength(64)]
		public string Cognome { get; } = cognome;

		[StringLength(64)]
		[EmailAddress(ErrorMessage = "Indirizzo e-mail invalido")]
		public string? Email { get; } = email;

		[StringLength(64)]
		[Phone(ErrorMessage = "Numero di telefono invalido")]
		public string? Telefono { get; } = telefono;
	}
}