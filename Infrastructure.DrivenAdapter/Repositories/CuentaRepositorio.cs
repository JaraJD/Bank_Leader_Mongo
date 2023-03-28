using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.DrivenAdapter.Repositories
{
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly IMongoCollection<CuentaMongo> coleccion;
        private readonly IMapper _mapper;

        public CuentaRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Cuentas;
            _mapper = mapper;
        }

        public async Task<InsertarNuevaCuenta> InsertarCuentaAsync(InsertarNuevaCuenta cuenta)
        {
            var cuentaguardar = _mapper.Map<CuentaMongo>(cuenta);
            await coleccion.InsertOneAsync(cuentaguardar);
            return cuenta;
        }

        public async Task<List<Cuenta>> TraerTodasLasCuentas()
        {
            throw new NotImplementedException();
        }
    }
}
