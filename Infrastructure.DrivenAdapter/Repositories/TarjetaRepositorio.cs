using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Repositories
{
	public class TarjetaRepositorio : ITarjetaRepositorio
	{
		private readonly IMongoCollection<TarjetaMongo> coleccion;
		private readonly IMapper _mapper;

		public TarjetaRepositorio(IContext context, IMapper mapper)
		{
			coleccion = context.Tarjetas;
			_mapper = mapper;
		}

		public async Task<InsertarNuevaTarjeta> InsertarTarjetaAsync(InsertarNuevaTarjeta tarjeta)
		{
			var guardarTrajeta = _mapper.Map<TarjetaMongo>(tarjeta);
			await coleccion.InsertOneAsync(guardarTrajeta);
			return tarjeta;
		}

		public async Task<List<Tarjeta>> TraerTodasLasTarjetas()
		{
			var tarjetas = await coleccion.FindAsync(Builders<TarjetaMongo>.Filter.Empty);
			var listaTarjetas = tarjetas.ToEnumerable().Select(x => _mapper.Map<Tarjeta>(x)).ToList();
			return listaTarjetas;
		}
	}
}
