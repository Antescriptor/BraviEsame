using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Collections.Generic;
using System;
using DAL.DTOs;

namespace Es016.API.Controllers
{
	/// <summary>
	/// Gestione dati prenotazioni
	/// </summary>
	/// <param name="prenotazioneService"></param>
	[ApiController]
	[Route("[controller]")]
	public class PrenotazioneController(PrenotazioneService prenotazioneService) : Controller
	{
		private readonly PrenotazioneService _prenotazioneService = prenotazioneService;

		/// <summary>
		/// Visualizza spettacoli disponibili in base al titolo data e ora d'inizio.
		/// Opzionalmente è possibile specificare il numero massimo di posti disponibili,
		/// che altrimenti è di 50.
		/// </summary>
		/// <param name="getAvailable"></param>
		/// <returns code="200">OK</returns>
		/// <returns code="404">Nessun spettacolo disponibile</returns>
		/// <returns code="500">Errore interno al server</returns>
		[HttpPost("spettacoli_disponibili")]
		public IActionResult GetAvailable([FromBody] APIPreConGetAvail getAvailable)
		{
			try
			{
				getAvailable.PostiMassimi ??= 50;

				List<Spettacolo>? spettacoliDisponibili = _prenotazioneService.GetAvailable(getAvailable.PostiMassimi.Value, getAvailable.Titolo, getAvailable.DataEOraInizio, out uint postiRimanenti);
				if (spettacoliDisponibili is not null)
				{
					//return Ok(spettacoliDisponibili);
					return new JsonResult(new { data = spettacoliDisponibili, message = $"Posti rimanenti: {postiRimanenti}" });
				}
				else
				{
					return NotFound($"Posti rimanenti: {postiRimanenti}\nNessuno spettacolo disponibile secondo i criteri dati");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Inserisci nuova prenotazione.
		/// Il numero di posti massimi allo spettacolo è opzionale e il valore preimpostato è 50.
		/// </summary>
		/// <param name="add"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="201">Prenotazione inserita con successo</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpPost]
		public IActionResult Add([FromBody] APIPreConAdd add)
		{
			try
			{
				add.PostiMassimi ??= 50;

				if (_prenotazioneService.AddFirstAvailable(add.PostiMassimi.Value, out uint postiRimanenti, add.Titolo, add.DataEOraInizio, add.Posto, add.IdCliente, add.Nome, add.Cognome, add.Email, add.Telefono))
				{
					return StatusCode(StatusCodes.Status201Created, $"Posti rimanenti: {postiRimanenti}\nPrenotazione inserita con successo");
				}
				else
				{
					return BadRequest($"Posti rimanenti: {postiRimanenti}\nImpossibile inserire la prenotazione. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}



		/// <summary>
		/// Inserisci nuova prenotazione senza controlli
		/// </summary>
		/// <param name="prenotazioneDaAggiungere"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="201">Prenotazione inserita con successo</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpPost("inserimento_senza_controlli")]
		public IActionResult Add([FromBody] Prenotazione prenotazioneDaAggiungere)
		{
			if (prenotazioneDaAggiungere is null)
			{
				return BadRequest("Dati della prenotazione non validi.");
			}
			try
			{
				if (_prenotazioneService.Add(prenotazioneDaAggiungere))
				{
					return StatusCode(StatusCodes.Status201Created, "Prenotazione inserita con successo");
				}
				else
				{
					return BadRequest("Impossibile inserire la prenotazione. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Cancella prenotazione
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="200">OK</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] uint id)
		{
			try
			{
				if (_prenotazioneService.Delete(id))
				{
					return Ok("Prenotazione cancellata con successo");
				}
				else
				{
					return BadRequest("Impossibile cancellare la prenotazione. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Visualizza lista delle prenotazioni
		/// </summary>
		/// <response code="200">OK</response>
		/// <response code="500">Errore interno al server</response>
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				IEnumerable<Prenotazione> prenotazioni = _prenotazioneService.Get();

				return Ok(prenotazioni);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Visualizza prenotazione in base all'id dato
		/// </summary>
		/// <param name="id"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Prenotazione non trovata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpGet("{id}")]
		public IActionResult Get([FromRoute] uint id)
		{
			try
			{
				Prenotazione? prenotazioneDaOttenere = _prenotazioneService.Get(id);
				if (prenotazioneDaOttenere is not null)
				{
					return Ok(prenotazioneDaOttenere);
				}
				else
				{
					return NotFound("Prenotazione non trovata");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Aggiorna prenotazione
		/// </summary>
		/// <param name="prenotazioneDaAggiornare"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="200">Prenotazione aggiornata con successo</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpPut]
		public IActionResult Update([FromBody] Prenotazione prenotazioneDaAggiornare)
		{
			if (prenotazioneDaAggiornare is null)
			{
				return BadRequest("Dati del cliente non validi.");
			}
			try
			{
				if (_prenotazioneService.Update(prenotazioneDaAggiornare))
				{
					return StatusCode(200, "Cliente aggiornato con successo");
				}
				else
				{
					return BadRequest("Impossibile aggiornare il cliente. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}