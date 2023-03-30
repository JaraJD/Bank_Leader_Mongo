using Ardalis.GuardClauses;
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
		private readonly IMongoCollection<TransaccionMongo> coleccionTransaccion;
		private readonly IMapper _mapper;

		public TarjetaRepositorio(IContext context, IMapper mapper)
		{
			coleccion = context.Tarjetas;
			coleccionTransaccion = context.Transacciones;
			_mapper = mapper;
		}

		public async Task<InsertarNuevaTarjeta> InsertarTarjetaAsync(InsertarNuevaTarjeta tarjeta)
		{
			Guard.Against.Null(tarjeta, nameof(tarjeta));
			Guard.Against.NullOrEmpty(tarjeta.Cliente_Id.ToString(), nameof(tarjeta.Cliente_Id));
			Guard.Against.NullOrEmpty(tarjeta.Tipo_Tarjeta, nameof(tarjeta.Tipo_Tarjeta));
			Guard.Against.NullOrEmpty(tarjeta.Fecha_Emision.ToString(), nameof(tarjeta.Fecha_Emision));
			Guard.Against.NullOrEmpty(tarjeta.Fecha_Vencimiento.ToString(), nameof(tarjeta.Fecha_Vencimiento));
			Guard.Against.NullOrEmpty(tarjeta.Limite_Credito.ToString(), nameof(tarjeta.Limite_Credito));
			Guard.Against.NullOrEmpty(tarjeta.Estado, nameof(tarjeta.Estado));

			var guardarTrajeta = _mapper.Map<TarjetaMongo>(tarjeta);
			await coleccion.InsertOneAsync(guardarTrajeta);

			var transaccionTarjeta = new Transaccion
			{
				Cuenta_Id = (string?)null,
				Tarjeta_Id = guardarTrajeta.Tarjeta_Id,
				Producto_Id = (string?)null,
				Fecha = DateTime.Now,
				Tipo_Transaccion = "Creacion de Tarjeta",
				Descripcion = "Se crea la tarjeta del usuario con el cliente_id " + tarjeta.Cliente_Id + ".   ",  // El espacio al final es para que el campo tenga el mismo tamaño que los otros campos de la tabla.
				Monto = tarjeta.Limite_Credito
			};

			var transaccionAguardar = _mapper.Map<TransaccionMongo>(transaccionTarjeta);
			await coleccionTransaccion.InsertOneAsync(transaccionAguardar);

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
