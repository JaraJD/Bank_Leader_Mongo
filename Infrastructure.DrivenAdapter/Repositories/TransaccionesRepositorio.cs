using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Entities;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.DrivenAdapter.Repositories
{
    public class TransaccionesRepositorio : ITransaccionesRepositorio
    {
        private readonly IMongoCollection<TransaccionMongo> coleccion;
        private readonly IMapper _mapper;

        public TransaccionesRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Transacciones;
            _mapper = mapper;
        }

        public async Task<List<Transaccion>> TraerTodasLasTransacciones()
        {
            var transacciones = await coleccion.FindAsync(Builders<TransaccionMongo>.Filter.Empty);
            var listaTransacciones = transacciones.ToEnumerable().Select(x => _mapper.Map<Transaccion>(x)).ToList();
            return listaTransacciones;
        }
    }
}
