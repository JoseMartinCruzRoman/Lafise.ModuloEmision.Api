using Microsoft.AspNetCore.Mvc;
using Lafise.ModuloEmision.Api.Models.DTOs;
using Lafise.ModuloEmision.Api.Services;
using Lafise.ModuloEmision.Api.Data.Repositories;
using System;

namespace Lafise.ModuloEmision.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizaController : ControllerBase
    {
        private readonly EmisionService _emisionService;
        private readonly IPolizaRepository _polizaRepository;

        public PolizaController(EmisionService emisionService, IPolizaRepository polizaRepository)
        {
            _emisionService = emisionService;
            _polizaRepository = polizaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> HistorialPolizas()
        {
            var polizas = await _polizaRepository.ObtenerPolizasAsync();
            return Ok(polizas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDetalle(int id)
        {
            var poliza = await _emisionService.ObtenerPolizaAsync(id);
            if (poliza == null)
            {
                return NotFound(new { mensaje = $"La poliza no fue econtrada" });
            }

            return Ok(poliza);
        }

        [HttpPost("emitir")]
        public async Task<IActionResult> Emitir([FromBody] EmisionPolizaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var polizaEmitida = await _emisionService.EmitirPolizaAutomovilAsync(dto);
                return CreatedAtAction(nameof(ObtenerDetalle), new { id = polizaEmitida.Id }, polizaEmitida);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al emitir la póliza: {ex.Message}" });
            }
        }
    }
}