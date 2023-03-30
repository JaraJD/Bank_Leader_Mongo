using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.EntitiesMongo
{
	internal class ActivosCliente
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Cliente_Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime Fecha_Nacimiento { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Genero { get; set; }
		public List<ObjectId> Cuentas { get; set; }
		public List<ObjectId> Tarjetas { get; set; }
		public List<ObjectId> Productos { get; set; }
	}
}
