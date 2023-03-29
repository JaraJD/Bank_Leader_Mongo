using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.DrivenAdapter.EntitiesMongo
{
    public class TransaccionMongo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Transaccion_Id { get; set; }
        public string Cuenta_Id { get; set; }
        public string? Tarjeta_Id { get; set; }
        public string? Producto_Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo_Transaccion { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}
