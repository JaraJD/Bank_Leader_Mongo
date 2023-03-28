﻿using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway
{
    public interface IProductoCasoDeUso
    {
        Task<List<Producto>> ObtenerProductos();
        Task<InsertarNuevoProducto> AgregarProducto(InsertarNuevoProducto producto);
    }
}
