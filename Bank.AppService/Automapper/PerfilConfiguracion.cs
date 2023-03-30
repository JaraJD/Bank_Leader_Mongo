﻿using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.Entities.Entities.Transacciones;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using System.IO;
using System.Text.RegularExpressions;

namespace Bank.AppService.Automapper
{
    public class PerfilConfiguracion : Profile
    {
        public PerfilConfiguracion()
        {
            CreateMap<InsertarNuevaCuenta, Cuenta>().ReverseMap();
            CreateMap<CuentaMongo, InsertarNuevaCuenta>().ReverseMap();
            CreateMap<CuentaMongo, Cuenta>().ReverseMap();

            CreateMap<InsertarNuevaTarjeta, Tarjeta>().ReverseMap();
            CreateMap<TarjetaMongo, InsertarNuevaTarjeta>().ReverseMap();
			CreateMap<TarjetaMongo, Tarjeta>().ReverseMap();

            CreateMap<InsertarNuevaTransaccion, Transaccion>().ReverseMap();
            CreateMap<TransaccionMongo, InsertarNuevaTransaccion>().ReverseMap();
            CreateMap<TransaccionMongo, Transaccion>().ReverseMap();

			CreateMap<InsertarNuevoCliente, Cliente>().ReverseMap();
            CreateMap<ClienteMongo, InsertarNuevoCliente>().ReverseMap();
            CreateMap<ClienteMongo, Cliente>().ReverseMap();

            CreateMap<InsertarNuevoProducto, Producto>().ReverseMap();
            CreateMap<ProductoMongo, InsertarNuevoProducto>().ReverseMap();
            CreateMap<ProductoMongo, Producto>().ReverseMap();

            CreateMap<ClienteMongo, ClienteConCuenta>()
                .ForMember(dest => dest.Cuentas, opt => opt.Ignore());

            CreateMap<CuentaMongo, CuentaConTransaccion>()
                .ForMember(dest => dest.Transacciones, opt => opt.Ignore());

            CreateMap<TransaccionMongo, TransaccionCuenta>();

        }

    }
}
