using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.Entities.Entities.Transacciones;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.DrivenAdapter.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        IMongoCollection<ClienteMongo> coleccion;
        private readonly IMongoCollection<CuentaMongo> coleccionCuentas;
        private readonly IMongoCollection<TransaccionMongo> coleccionTransacciones;
        private readonly IMapper _mapper;

        public ClienteRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Clientes;
            coleccionCuentas = context.Cuentas;
            coleccionTransacciones = context.Transacciones;
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

        public async Task<Cliente> ObtenerClientePorIdAsync(string cliente_id)
        {
            var filter = Builders<ClienteMongo>.Filter.Eq(c => c.Cliente_Id, cliente_id);
            var clienteMongo = await coleccion.Find(filter).FirstOrDefaultAsync();

            if (clienteMongo == null)
            {
                return null;
            }

            var cliente = _mapper.Map<Cliente>(clienteMongo);
            return cliente;
        }

        public async Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
        {
            var clientesMongo = await coleccion.FindAsync(Builders<ClienteMongo>.Filter.Empty);
            var clientes = clientesMongo.ToEnumerable().Select(x => _mapper.Map<ClienteConCuenta>(x)).ToList();

            var cuentasMongo = await coleccionCuentas.FindAsync(Builders<CuentaMongo>.Filter.Empty);
            var cuentas = cuentasMongo.ToEnumerable().Select(x => _mapper.Map<CuentaConTransaccion>(x)).ToList();

            var transaccionesMongo = await coleccionTransacciones.FindAsync(Builders<TransaccionMongo>.Filter.Empty);
            var transacciones = transaccionesMongo.ToEnumerable().Select(x => _mapper.Map<TransaccionCuenta>(x)).ToList();

            var clientesDic = new Dictionary<string, ClienteConCuenta>();

            foreach (var cliente in clientes)
            {
                var cuentasCliente = cuentas.Where(c => c.Cliente_Id == cliente.Cliente_Id).ToList();
                cliente.Cuentas = cuentasCliente;

                foreach (var cuenta in cuentasCliente)
                {
                    cuenta.Transacciones = transacciones.Where(t => t.Cuenta_Id == cuenta.Cuenta_Id).ToList();
                }

                clientesDic.Add(cliente.Cliente_Id, cliente);
            }

            return clientesDic.Values.ToList();
        }

        public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
        {
            throw new NotImplementedException();
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
