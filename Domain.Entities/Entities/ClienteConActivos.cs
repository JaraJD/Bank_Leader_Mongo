﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
	public class ClienteConActivos
	{
		public string Cliente_Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime Fecha_Nacimiento { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Genero { get; set; }
		public List<Cuenta> Cuentas { get; set; }
		public List<Producto> Productos { get; set; }
		public List<Tarjeta> Tarjetas { get; set; }
	}
}
