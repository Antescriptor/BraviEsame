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
	/// Gestione dati spettacoli
	/// </summary>
	/// <param name="spettacoloService"></param>
	[ApiController]
	[Route("[controller]")]
	public class SpettacoloController(SpettacoloService spettacoloService) : Controller
	{
		private readonly SpettacoloService _spettacoloService = spettacoloService;

		/// <summary>
		/// Inserisci nuovo spettacolo
		/// </summary>
		/// <param name="add"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="201">Spettacolo inserito con successo</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>

		[HttpPost]
		public IActionResult Add(APITaConAdd add)
		{
			try
			{
				Spettacolo spettacoloDaAggiungere = new(_spettacoloService.GetNextId(), add.Titolo, add.Descrizione, add.DataEOra, add.Durata, add.PrezzoBase);

				if (_spettacoloService.Add(spettacoloDaAggiungere))
				{
					return StatusCode(StatusCodes.Status201Created, "Spettacolo inserito con successo");
				}
				else
				{
					return BadRequest("Impossibile inserire lo spettacolo. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Cancella spettacolo
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
				if (_spettacoloService.Delete(id))
				{
					return Ok("Spettacolo cancellato con successo");
				}
				else
				{
					return BadRequest("Impossibile cancellare lo spettacolo. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Visualizza lista degli spettacoli
		/// </summary>
		/// <returns>Vedi sommario</returns>
		/// <response code="200">OK</response>
		/// <response code="500">Errore interno al server</response>
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				IEnumerable<Spettacolo> spettacoli = _spettacoloService.Get();

				return Ok(spettacoli);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Visualizza spettacolo in base all'id dato
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Vedi sommario</returns>
		/// <response code="200">OK</response>
		/// <response code="404">Spettacolo non trovato</response>
		/// <response code="500">Errore interno al server</response>
		[HttpGet("{id}")]
		public IActionResult Get([FromRoute] uint id)
		{
			try
			{
				Spettacolo? spettacoloDaOttenere = _spettacoloService.Get(id);
				if (spettacoloDaOttenere is not null)
				{
					return Ok(spettacoloDaOttenere);
				}
				else
				{
					return NotFound("Spettacolo non trovato");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Aggiorna spettacolo
		/// </summary>
		/// <param name="spettacoloDaAggiornare"></param>
		/// <returns>Un messaggio di conferma dell'operazione</returns>
		/// <response code="200">Spettacolo aggiornato con successo</response>
		/// <response code="400">Richiesta malformata</response>
		/// <response code="500">Errore interno al server</response>
		[HttpPut]
		public IActionResult Update([FromBody] Spettacolo spettacoloDaAggiornare)
		{
			if (spettacoloDaAggiornare is null)
			{
				return BadRequest("Dati dello spettacolo non validi.");
			}
			try
			{
				if (_spettacoloService.Update(spettacoloDaAggiornare))
				{
					return Ok("Spettacolo aggiornato con successo");
				}
				else
				{
					return BadRequest("Impossibile aggiornare lo spettacolo. Verificare i dati e riprovare.");
				}
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}