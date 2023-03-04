using L01_2020_VH_602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_VH_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;
        public PedidosController(restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listaPedidos = (from e in _restauranteContext.pedidos
                                            select e).ToList();
            if (listaPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaPedidos);
        }

        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult Get(int id)
        {
            pedidos? pedidos = (from e in _restauranteContext.pedidos
                                    where e.pedidoId == id
                                    select e).FirstOrDefault();
            if (pedidos == null)
            {
                return NotFound();
            }

            return Ok(pedidos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContext.pedidos.Add(pedido);
                _restauranteContext.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedido)
        {
            pedidos? pedidoAct = (from e in _restauranteContext.pedidos
                                       where e.pedidoId == id
                                       select e).FirstOrDefault();

            if (pedidoAct == null) { return NotFound(); }

            pedidoAct.motoristaId = pedido.motoristaId;
            pedidoAct.clienteId = pedido.clienteId;
            pedidoAct.platoId = pedido.platoId;
            pedidoAct.cantidad = pedido.cantidad;
            pedidoAct.precio = pedido.precio;

            _restauranteContext.Entry(pedidoAct).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(pedidoAct);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult BorrarPedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                                 where e.pedidoId == id
                                 select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            _restauranteContext.Attach(pedido);
            _restauranteContext.Remove(pedido);
            _restauranteContext.SaveChanges();
            return Ok(pedido);
        }
        [HttpGet]
        [Route("FilterByCliente/{idCliente}")]
        public IActionResult GetByCliente(int idCliente)
        {
            List<pedidos> pedidos = (from e in _restauranteContext.pedidos
                                     where e.pedidoId == idCliente
                                     select e).ToList();
            if (pedidos == null)
            {
                return NotFound();
            }

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("FilterByMotorista/{idMotorista}")]
        public IActionResult GetByMotorista(int idMotorista)
        {
            List<pedidos> pedidos = (from e in _restauranteContext.pedidos
                                where e.pedidoId == idMotorista
                                select e).ToList();
            if (pedidos == null)
            {
                return NotFound();
            }

            return Ok(pedidos);
        }

    }
}
