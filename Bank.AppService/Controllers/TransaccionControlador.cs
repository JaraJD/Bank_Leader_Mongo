using AutoMapper;
using Domain.Entities.Entities;
using Domain.UseCase.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace Bank.AppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionControlador : Controller
    {
        private readonly ITransaccionCasoDeUso _transaccionesCasodeUso;
        private readonly IMapper _mapper;

        public TransaccionControlador(ITransaccionCasoDeUso transaccionesCasodeUso, IMapper mapper)
        {
            _transaccionesCasodeUso = transaccionesCasodeUso;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Transaccion>> ObtenerTransacciones()
        {
            return await _transaccionesCasodeUso.ObtenerTransacciones();
        }
    }
}
