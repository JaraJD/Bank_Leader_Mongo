using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.DrivenAdapter.Repositories
{
    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly IMongoCollection<CuentaMongo> coleccion;
        private readonly IMongoCollection<TransaccionMongo> coleccionTransaccion;
		private readonly IMapper _mapper;

        public CuentaRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Cuentas;
			coleccionTransaccion = context.Transacciones;
			_mapper = mapper;
        }

		public IMongoCollection<CuentaMongo> GetColeccion()
		{
			return coleccion;
		}

		public async Task<InsertarNuevaCuenta> InsertarCuentaAsync(InsertarNuevaCuenta cuenta)
        {

			Guard.Against.Null(cuenta, nameof(cuenta));
			Guard.Against.NullOrEmpty(cuenta.Cliente_Id.ToString(), nameof(cuenta.Cliente_Id));
			Guard.Against.NullOrEmpty(cuenta.Tipo_Cuenta, nameof(cuenta.Tipo_Cuenta));
			Guard.Against.NullOrEmpty(cuenta.Saldo.ToString(), nameof(cuenta.Saldo));
			Guard.Against.NullOrEmpty(cuenta.Fecha_Apertura.ToString(), nameof(cuenta.Fecha_Apertura));
			Guard.Against.NullOrEmpty(cuenta.Fecha_Cierre.ToString(), nameof(cuenta.Fecha_Cierre));
			Guard.Against.NullOrEmpty(cuenta.Tasa_Interes.ToString(), nameof(cuenta.Tasa_Interes));
			Guard.Against.NullOrEmpty(cuenta.Estado, nameof(cuenta.Estado));

			var cuentaGuardar = _mapper.Map<CuentaMongo>(cuenta);
            await coleccion.InsertOneAsync(cuentaGuardar);

            var transaccionCuenta = new Transaccion
			{
                Cuenta_Id = cuentaGuardar.Cuenta_Id,
				Tarjeta_Id = (string?)null,
				Producto_Id = (string?)null,
				Fecha = DateTime.Now,
                Tipo_Transaccion = "Creacion de cuenta",
                Descripcion = "Se crea la cuenta del usuario con el cliente_id " + cuenta.Cliente_Id + ".   ",  // El espacio al final es para que el campo tenga el mismo tamaño que los otros campos de la tabla.
                Monto = cuenta.Saldo
            };

            var transaccionAguardar = _mapper.Map<TransaccionMongo>(transaccionCuenta);
            await coleccionTransaccion.InsertOneAsync(transaccionAguardar);

            return cuenta;
        }

        public async Task<List<Cuenta>> TraerTodasLasCuentas()
        {
            var cuentas = await coleccion.FindAsync(Builders<CuentaMongo>.Filter.Empty);
            var listaCuentas = cuentas.ToEnumerable().Select(x => _mapper.Map<Cuenta>(x)).ToList();
            return listaCuentas;
        }
    }
}
