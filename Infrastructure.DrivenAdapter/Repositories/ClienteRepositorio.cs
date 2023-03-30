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
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IMongoCollection<ClienteMongo> coleccion;
        private readonly IMongoCollection<TarjetaMongo> coleccionTarjetas;
		private readonly IMapper _mapper;

        public ClienteRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Clientes;
            coleccionTarjetas = context.Tarjetas;
			_mapper = mapper;
        }

        public async Task<InsertarNuevoCliente> InsertarClienteAsync(InsertarNuevoCliente cliente)
        {
            var guardarCliente = _mapper.Map<ClienteMongo>(cliente);
            await coleccion.InsertOneAsync(guardarCliente);
            return cliente;
        }

        public async Task<List<Cliente>> TraerTodosLosClientesAsync()
        {
            var clientes = await coleccion.FindAsync(Builders<ClienteMongo>.Filter.Empty);
            var listaClientes = clientes.ToEnumerable().Select(x => _mapper.Map<Cliente>(x)).ToList();
            return listaClientes;
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
        { 
		    //var tarjetas = await coleccionTarjetas.FindAsync(Builders<TarjetaMongo>.Filter.Empty);
		    //var listaTarjetas = tarjetas.ToEnumerable().Select(x => _mapper.Map<Tarjeta>(x)).ToList();
        
			var clientes = await coleccion.FindAsync(Builders<ClienteMongo>.Filter.Empty);
			var listaClientes = clientes.ToEnumerable().Select(x => _mapper.Map<ClienteConTarjeta>(x)).ToList();

			foreach (var cliente in listaClientes)
            {
                Console.WriteLine(cliente);
            }


			return listaClientes;
		}

        public async Task<List<ClienteConProducto>> ObtenerClienteProductoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteConActivos> ObtenerClienteActivosAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
