using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter
{
	public class Context : IContext
	{
		private readonly IMongoDatabase _database;

		public Context(string stringConnection, string DBname)
		{
			MongoClient cliente = new MongoClient(stringConnection);
			_database = cliente.GetDatabase(DBname);
		}

		public IMongoCollection<ClienteMongo> Clientes => _database.GetCollection<ClienteMongo>("clientes");

		public IMongoCollection<CuentaMongo> Cuentas => _database.GetCollection<CuentaMongo>("cuentas");

		public IMongoCollection<ProductoMongo> Productos => _database.GetCollection<ProductoMongo>("productos");

		public IMongoCollection<TarjetaMongo> Tarjetas => _database.GetCollection<TarjetaMongo>("tarjetas");

		public IMongoCollection<TransaccionMongo> Transacciones => _database.GetCollection<TransaccionMongo>("transacciones");
	}
}
