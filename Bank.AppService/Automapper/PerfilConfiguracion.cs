using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
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
            CreateMap<CuentaMongo, Cuenta>().ReverseMap();

            CreateMap<InsertarNuevaTarjeta, Tarjeta>().ReverseMap();
            CreateMap<TarjetaMongo, Tarjeta>().ReverseMap();

            CreateMap<InsertarNuevaTransaccion, Transaccion>().ReverseMap();
            CreateMap<TransaccionMongo, Transaccion>().ReverseMap();

            CreateMap<InsertarNuevoCliente, Cliente>().ReverseMap();
            CreateMap<ClienteMongo, InsertarNuevoCliente>().ReverseMap();
            CreateMap<ClienteMongo, Cliente>().ReverseMap();

            CreateMap<InsertarNuevoProducto, Producto>().ReverseMap();
            CreateMap<ProductoMongo, Producto>().ReverseMap();

        }

    }
}
