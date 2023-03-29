using AutoMapper;
using Domain.Entities.Commands;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.AppService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TarjetaControlador : ControllerBase
	{
		private readonly ITarjetaCasoDeUso _tarjetaCasoDeUso;
		private readonly IMapper _mapper;

		public TarjetaControlador(ITarjetaCasoDeUso tarjetaCaspDeUso, IMapper mapper)
		{
			_tarjetaCasoDeUso = tarjetaCaspDeUso;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<Tarjeta>> Obtener_Listado_Trajetas()
		{
			return await _tarjetaCasoDeUso.ObtenerTarjetas();
		}

		[HttpPost]
		public async Task<InsertarNuevaTarjeta> Registrar_Tarjeta(InsertarNuevaTarjeta command)
		{
			return await _tarjetaCasoDeUso.AgregarTarjeta(command);
		}
	}
}
