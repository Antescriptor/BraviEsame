using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Models
{
	public class Cliente(uint id, string nome, string cognome, string? email = null, string? telefono = null)
	{
		public uint Id { get; set; } = id;
		public string Nome { get; set; } = nome;
		public string Cognome { get; set; } = cognome;

		[Display(Name = "Indirizzo e-mail")]
		[EmailAddress(ErrorMessage = "Indirizzo e-mail invalido")]
		public string? Email { get; set; } = email;


		[Display(Name = "Numero di telefono")]
		[Phone(ErrorMessage = "Numero di telefono invalido")]
		public string? Telefono { get; set; } = telefono;

		[JsonIgnore]
		public ICollection<Prenotazione> Prenotazioni { get; } = new List<Prenotazione>();
	}
}
