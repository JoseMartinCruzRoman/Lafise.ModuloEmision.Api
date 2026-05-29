using Lafise.ModuloEmision.Api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lafise.ModuloEmision.Api.Data.Repositories
{
    public interface IPolizaRepository
    {
        Task<Poliza> GuardarPolizaAsync(Poliza poliza);
        Task<List<Poliza>> ObtenerPolizasAsync();
    }
}