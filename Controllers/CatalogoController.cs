using Lafise.ModuloEmision.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lafise.ModuloEmision.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CatalogosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }
        [HttpGet("coberturas")]
        public async Task<IActionResult> GetCoberturas()
        {
            var coberturas = await _context.Coberturas.ToListAsync();
            return Ok(coberturas);
        }
    }
}