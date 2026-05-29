using Lafise.ModuloEmision.Api.Data;
using Lafise.ModuloEmision.Api.Data.Repositories;
using Lafise.ModuloEmision.Api.Models.DTOs;
using Lafise.ModuloEmision.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Lafise.ModuloEmision.Api.Services
{
    public class EmisionService
    {
        private readonly IPolizaRepository _polizaRepository;
        private readonly ApplicationDbContext _context;

        public EmisionService(IPolizaRepository polizaRepository, ApplicationDbContext context)
        {
            _polizaRepository = polizaRepository;
            _context = context;
        }

        public async Task<Poliza> EmitirPolizaAutomovilAsync(EmisionPolizaDto datos)
        {

            //verificamos que el cliente exista
            var cliente = await _context.Clientes.FindAsync(datos.ClienteId);
            if (cliente == null)
            {
                throw new InvalidOperationException("El cliente con la identificación proporcionada no existe.");
            }

            // verificamos que la antiguedad del vehiculo no pase de msa de 20 años
            int anioActual = DateTime.Now.Year;
            if (datos.Anio < (anioActual - 20))
            {
                throw new InvalidOperationException("El vehículo no puede tener más de 20 años de antigüedad.");
            }

            // verificar si existe una matricula igual en el sistema con una poliza activa
            bool matriculaConPolizaActiva = await _context.Polizas
                .AnyAsync(p => p.Vehiculo.Matricula == datos.Matricula && p.EsActivo);

            if (matriculaConPolizaActiva)
            {
                throw new InvalidOperationException("Ya existe una póliza activa para la matrícula proporcionada.");
            }

            // generamos el numero de poliza
            // contamos el numero de polizas existententes para generar un numero unico
            int totalPolizas = await _context.Polizas.CountAsync();
            string numeroPoliza = $"POL-{DateTime.Now.Year}-{(totalPolizas + 1).ToString("D4")}";

            // registramos el vehículo
            var vehiculo = new Vehiculo
            {
                Matricula = datos.Matricula,
                Marca = datos.Marca,
                Modelo = datos.Modelo,
                Anio = datos.Anio,
                ValorComercial = datos.ValorComercial
            };

            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            // caculamos la priam total
            decimal sumaCoberturas = 0;
            var coberturasSeleccionadas = await _context.Coberturas
                .Where(c => datos.CoberturasIds.Contains(c.Id))
                .ToListAsync();

            foreach (var cobertura in coberturasSeleccionadas)
            {
                sumaCoberturas += cobertura.Tasa;
            }

            decimal primaCalculada = vehiculo.ValorComercial * (sumaCoberturas / 100);

            //creamos la poliza 
            var poliza = new Poliza
            {
                NumeroPoliza = numeroPoliza,
                FechaEmision = DateTime.Now,
                PrimaTotal = primaCalculada,
                EsActivo = true,
                ClienteId = cliente.Id,
                VehiculoId = vehiculo.Id,
                SumaAsegurada = vehiculo.ValorComercial
            };

            var polizaGuardada = await _polizaRepository.GuardarPolizaAsync(poliza);

            // registrar las coberturas asociadas a la póliza
            foreach (var cobertura in coberturasSeleccionadas)
            {
                var polizaCobertura = new PolizaCobertura
                {
                    PolizaId = polizaGuardada.Id,
                    CoberturaId = cobertura.Id
                };
                _context.PolizaCoberturas.Add(polizaCobertura);
            }

            await _context.SaveChangesAsync();
            return polizaGuardada;
        }

        public async Task<Poliza?> ObtenerPolizaAsync(int id)
        {
            return await _context.Polizas
                .Include(p => p.Cliente)
                .Include(p => p.Vehiculo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}