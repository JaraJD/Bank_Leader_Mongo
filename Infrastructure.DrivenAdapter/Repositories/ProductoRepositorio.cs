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
	public class ProductoRepositorio : IProductoRepositorio
	{
		private readonly IMongoCollection<ProductoMongo> coleccion;
		private readonly IMongoCollection<TransaccionMongo> coleccionTransaccion;
		private readonly IMapper _mapper;

		public ProductoRepositorio(IContext context, IMapper mapper)
		{
			coleccion = context.Productos;
			coleccionTransaccion = context.Transacciones;
			_mapper = mapper;
		}

		public async Task<InsertarNuevoProducto> InsertarProductoAsync(InsertarNuevoProducto producto)
		{

			Guard.Against.Null(producto, nameof(producto));
			Guard.Against.NullOrEmpty(producto.Cliente_Id.ToString(), nameof(producto.Cliente_Id));
			Guard.Against.NullOrEmpty(producto.Tipo_Producto, nameof(producto.Tipo_Producto));
			Guard.Against.NullOrEmpty(producto.Descripcion, nameof(producto.Descripcion));
			Guard.Against.NullOrEmpty(producto.Plazo.ToString(), nameof(producto.Plazo));
			Guard.Against.NullOrEmpty(producto.Monto.ToString(), nameof(producto.Monto));
			Guard.Against.NullOrEmpty(producto.Tasa_Interes.ToString(), nameof(producto.Tasa_Interes));
			Guard.Against.NullOrEmpty(producto.Estado, nameof(producto.Estado));

			var guardarProducto = _mapper.Map<ProductoMongo>(producto);
			await coleccion.InsertOneAsync(guardarProducto);

			var transaccionProducto = new Transaccion
			{
				Cuenta_Id = (string?)null,
				Tarjeta_Id = (string?)null,
				Producto_Id = guardarProducto.Producto_Id,
				Fecha = DateTime.Now,
				Tipo_Transaccion = "Creacion de Producto",
				Descripcion = "Se crea el producto del usuario con el cliente_id " + producto.Cliente_Id + ".   ",  // El espacio al final es para que el campo tenga el mismo tamaño que los otros campos de la tabla.
				Monto = producto.Monto
			};

			var transaccionAguardar = _mapper.Map<TransaccionMongo>(transaccionProducto);
			await coleccionTransaccion.InsertOneAsync(transaccionAguardar);
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
