﻿using Domain.Entities.Commands;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Gateway
{
    public interface ICuentaCasoDeUso
    {
        Task<List<Cuenta>> ObtenerCuentas();

        Task<InsertarNuevaCuenta> AgregarCuenta(InsertarNuevaCuenta cuenta);
    }
}
