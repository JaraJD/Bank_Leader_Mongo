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
	public class ProductoRepositorio : IProductoRepositorio
	{
		private readonly IMongoCollection<ProductoMongo> coleccion;
		private readonly IMongoCollection<TransaccionMongo> coleccionTransaccion;
		private readonly IMapper _mapper;

		public ProductoRepositorio(IContext context, IMapper mapper)
		{
			coleccion = context.Productos;
			_mapper= mapper;
		}

		public async Task<InsertarNuevoProducto> InsertarProductoAsync(InsertarNuevoProducto producto)
		{
			var guardarProducto = _mapper.Map<ProductoMongo>(producto);
			await coleccion.InsertOneAsync(guardarProducto);
			return producto;
		}

		public async Task<List<Producto>> TraerTodosLosProductos()
		{
			var productos = await coleccion.FindAsync(Builders<ProductoMongo>.Filter.Empty);
			var listaProductos = productos.ToEnumerable().Select(x => _mapper.Map<Producto>(x)).ToList();
			return listaProductos;
		}
	}
}
