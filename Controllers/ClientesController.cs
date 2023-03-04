using L01_2020_VH_602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_VH_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public ClientesController(restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> listaClientes = (from e in _restauranteContext.clientes
                                          select e).ToList();
            if (listaClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaClientes);
        }

        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult Get(int id)
        {
            clientes? cliente = (from e in _restauranteContext.clientes
                                where e.clienteId == id
                                select e).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarCliente([FromBody] clientes cliente)
        {
            try
            {
                _restauranteContext.clientes.Add(cliente);
                _restauranteContext.SaveChanges();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] clientes cliente)
        {
            clientes? clienteAct = (from e in _restauranteContext.clientes
                                  where e.clienteId == id
                                  select e).FirstOrDefault();

            if (clienteAct == null) { return NotFound(); }

            clienteAct.nombreCliente = cliente.nombreCliente;
            clienteAct.direccion = cliente.direccion;

            _restauranteContext.Entry(clienteAct).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(clienteAct);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult BorrarCliente(int id)
        {
            clientes? cliente = (from e in _restauranteContext.clientes
                               where e.clienteId == id
                               select e).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }
            _restauranteContext.Attach(cliente);
            _restauranteContext.Remove(cliente);
            _restauranteContext.SaveChanges();
            return Ok(cliente);
        }

        [HttpGet]
        [Route("FilterByAddress/{address}")]
        public IActionResult GetByAddress(string address)
        {
            List<clientes> lstCliente = (from e in _restauranteContext.clientes
                                     where e.direccion.Contains(address)
                                     select e).ToList();
            if (lstCliente == null)
            {
                return NotFound();
            }

            return Ok(lstCliente);
        }
    }
}
