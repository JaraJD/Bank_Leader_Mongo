using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DrivenAdapter.EntitiesMongo
{
	public class ProductoMongo
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Producto_Id { get; set; }
		public string Cliente_Id { get; set; }
		public string Tipo_Producto { get; set; }
		public string Descripcion { get; set; }
		public int Plazo { get; set; }
		public decimal Monto { get; set; }
		public decimal Tasa_Interes { get; set; }
		public string Estado { get; set; }
	}
}
