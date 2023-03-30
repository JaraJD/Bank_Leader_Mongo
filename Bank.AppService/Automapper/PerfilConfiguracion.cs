using AutoMapper;
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
            CreateMap<ClienteMongo, ClienteConTarjeta>().ReverseMap();

			CreateMap<InsertarNuevoProducto, Producto>().ReverseMap();
            CreateMap<ProductoMongo, InsertarNuevoProducto>().ReverseMap();
            CreateMap<ProductoMongo, Producto>().ReverseMap();

            //Cuenta
            CreateMap<ClienteMongo, ClienteConCuenta>()
                .ForMember(dest => dest.Cuentas, opt => opt.Ignore());

            CreateMap<CuentaMongo, CuentaConTransaccion>()
                .ForMember(dest => dest.Transacciones, opt => opt.Ignore());

            CreateMap<TransaccionMongo, TransaccionCuenta>();
           
            //Tarjeta
            CreateMap<ClienteMongo, ClienteConTarjeta>()
                .ForMember(dest => dest.Tarjetas, opt => opt.Ignore());

            CreateMap<TarjetaMongo, TarjetaConTransaccion>()
                .ForMember(dest => dest.Transacciones, opt => opt.Ignore());

            CreateMap<TransaccionMongo, TransaccionTarjeta>();
           
            //Producto
            CreateMap<ClienteMongo, ClienteConProducto>()
                .ForMember(dest => dest.Productos, opt => opt.Ignore());

            CreateMap<ProductoMongo, ProductoConTransaccion>()
                .ForMember(dest => dest.Transacciones, opt => opt.Ignore());

            CreateMap<TransaccionMongo, TransaccionProducto>();
        }

    }
}
