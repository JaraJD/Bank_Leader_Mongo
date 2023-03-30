using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Transform;
using Ardalis.GuardClauses;
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
        private readonly IMongoCollection<TarjetaMongo> coleccionTarjetas;
        private readonly IMongoCollection<ProductoMongo> coleccionProductos;
        private readonly IMongoCollection<TransaccionMongo> coleccionTransacciones;
        private readonly IMapper _mapper;

        public ClienteRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Clientes;
            coleccionCuentas = context.Cuentas;
            coleccionTarjetas = context.Tarjetas;
            coleccionProductos = context.Productos;
            coleccionTransacciones = context.Transacciones;
            _mapper = mapper;
        }

        public async Task<InsertarNuevoCliente> InsertarClienteAsync(InsertarNuevoCliente cliente)
        {
			Guard.Against.Null(cliente, nameof(cliente));
			Guard.Against.NullOrEmpty(cliente.Nombre, nameof(cliente.Nombre), "Nombre requerido. ");
			Guard.Against.NullOrEmpty(cliente.Apellido, nameof(cliente.Apellido));
			Guard.Against.NullOrEmpty(cliente.Fecha_Nacimiento.ToString(), nameof(cliente.Fecha_Nacimiento));
			Guard.Against.NullOrEmpty(cliente.Telefono, nameof(cliente.Telefono));
			Guard.Against.NullOrEmpty(cliente.Correo, nameof(cliente.Correo));
			Guard.Against.NullOrEmpty(cliente.Genero, nameof(cliente.Genero));

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
            var clientesMongo = await coleccion.FindAsync(Builders<ClienteMongo>.Filter.Empty);
            var clientes = clientesMongo.ToEnumerable().Select(x => _mapper.Map<ClienteConTarjeta>(x)).ToList();

            var tarjetasMongo = await coleccionTarjetas.FindAsync(Builders<TarjetaMongo>.Filter.Empty);
            var cuentas = tarjetasMongo.ToEnumerable().Select(x => _mapper.Map<TarjetaConTransaccion>(x)).ToList();

            var transaccionesMongo = await coleccionTransacciones.FindAsync(Builders<TransaccionMongo>.Filter.Empty);
            var transacciones = transaccionesMongo.ToEnumerable().Select(x => _mapper.Map<TransaccionTarjeta>(x)).ToList();

            var clientesDic = new Dictionary<string, ClienteConTarjeta>();

            foreach (var cliente in clientes)
            {
                var tarjetasCliente = cuentas.Where(c => c.Cliente_Id == cliente.Cliente_Id).ToList();
                cliente.Tarjetas = tarjetasCliente;

                foreach (var tarjeta in tarjetasCliente)
                {
                    tarjeta.Transacciones = transacciones.Where(t => t.Tarjeta_Id == tarjeta.Tarjeta_Id).ToList();
                }

                clientesDic.Add(cliente.Cliente_Id, cliente);
            }

            return clientesDic.Values.ToList();
        }

        public async Task<List<ClienteConProducto>> ObtenerClienteProductoAsync()
        {
            var clientesMongo = await coleccion.FindAsync(Builders<ClienteMongo>.Filter.Empty);
            var clientes = clientesMongo.ToEnumerable().Select(x => _mapper.Map<ClienteConProducto>(x)).ToList();

            var productoMongo = await coleccionProductos.FindAsync(Builders<ProductoMongo>.Filter.Empty);
            var productos = productoMongo.ToEnumerable().Select(x => _mapper.Map<ProductoConTransaccion>(x)).ToList();

            var transaccionesMongo = await coleccionTransacciones.FindAsync(Builders<TransaccionMongo>.Filter.Empty);
            var transacciones = transaccionesMongo.ToEnumerable().Select(x => _mapper.Map<TransaccionProducto>(x)).ToList();

            var clientesDic = new Dictionary<string, ClienteConProducto>();

            foreach (var cliente in clientes)
            {
                var productosCliente = productos.Where(c => c.Cliente_Id == cliente.Cliente_Id).ToList();
                cliente.Productos = productosCliente;

                foreach (var producto in productosCliente)
                {
                    producto.Transacciones = transacciones.Where(t => t.Producto_Id == producto.Producto_Id).ToList();
                }

                clientesDic.Add(cliente.Cliente_Id, cliente);
            }

            return clientesDic.Values.ToList();
        }

        public async Task<ClienteConActivos> ObtenerClienteActivosAsync(string id)
        {
			var filter = Builders<ClienteMongo>.Filter.Eq(c => c.Cliente_Id, id);

			var cuentasTask = coleccionCuentas.Find(c => c.Cliente_Id == id && c.Saldo > 0).ToList();
			var tarjetasTask = coleccionTarjetas.Find(c => c.Cliente_Id == id && c.Limite_Credito > 0).ToList();
			var productosTask = coleccionProductos.Find(c => c.Cliente_Id == id).ToList();

			var cliente = await coleccion.Find(filter).FirstOrDefaultAsync();
            var clienteActivos = _mapper.Map<ClienteConActivos>(cliente);

            clienteActivos.Cuentas = _mapper.Map<List<Cuenta>>(cuentasTask);
            clienteActivos.Tarjetas = _mapper.Map<List<Tarjeta>>(tarjetasTask);
            clienteActivos.Productos = _mapper.Map<List<Producto>>(productosTask);

            return clienteActivos;
		}
    }
}
