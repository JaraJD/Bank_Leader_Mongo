﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway.Repository;
using Infrastructure.DrivenAdapter.EntitiesMongo;
using Infrastructure.DrivenAdapter.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.DrivenAdapter.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IMongoCollection<ClienteMongo> coleccion;
        private readonly IMapper _mapper;

        public ClienteRepositorio(IContext context, IMapper mapper)
        {
            coleccion = context.Clientes;
            _mapper = mapper;
        }

        public async Task<InsertarNuevoCliente> InsertarClienteAsync(InsertarNuevoCliente cliente)
        {
            var guardarCliente = _mapper.Map<ClienteMongo>(cliente);
            await coleccion.InsertOneAsync(guardarCliente);
            return _mapper.Map<InsertarNuevoCliente>(cliente);
        }

        public async Task<List<Cliente>> TraerTodosLosClientesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteConCuenta>> ObtenerClienteTransaccionesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteConTarjeta>> ObtenerClienteTarjetaAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClienteConProducto>> ObtenerClienteProductoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteConActivos> ObtenerClienteActivosAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
