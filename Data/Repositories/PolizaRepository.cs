using Microsoft.EntityFrameworkCore;
using Lafise.ModuloEmision.Api.Models.Entities;

namespace Lafise.ModuloEmision.Api.Data.Repositories
{
    public class PolizaRepository : IPolizaRepository
    {

        private readonly ApplicationDbContext _context;

        public PolizaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Poliza> GuardarPolizaAsync(Poliza poliza)
        {
            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();
            return poliza;
        }

        public async Task<List<Poliza>> ObtenerPolizasAsync()
        {
            return await _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.Vehiculo)
                .ToListAsync();
        }
    }
}