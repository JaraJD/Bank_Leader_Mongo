using Infrastructure.DrivenAdapter.EntitiesMongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.Interfaces
{
	public interface IContext
	{
		public IMongoCollection<ClienteMongo> Clientes { get; }
		public IMongoCollection<CuentaMongo> Cuentas { get; }
		public IMongoCollection<ProductoMongo> Productos { get; }
		public IMongoCollection<TarjetaMongo> Tarjetas { get; }
		public IMongoCollection<TransaccionMongo> Transacciones { get; }
	}
}
