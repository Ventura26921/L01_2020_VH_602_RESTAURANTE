using L01_2020_VH_602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_VH_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;
        public PlatosController(restauranteContext context)
        {
            _restauranteContext = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listaPlatos = (from e in _restauranteContext.platos
                                          select e).ToList();
            if (listaPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaPlatos);
        }

        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult Get(int id)
        {
            platos? plato = (from e in _restauranteContext.platos
                                where e.platoId == id
                                select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlato([FromBody] platos platos)
        {
            try
            {
                _restauranteContext.platos.Add(platos);
                _restauranteContext.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] platos plato)
        {
            platos? platoAct = (from e in _restauranteContext.platos
                                  where e.platoId == id
                                  select e).FirstOrDefault();

            if (platoAct == null) { return NotFound(); }

            platoAct.nombrePlato = plato.nombrePlato;
            platoAct.precio = plato.precio;

            _restauranteContext.Entry(platoAct).State = EntityState.Modified;
            _restauranteContext.SaveChanges();
            return Ok(platoAct);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult BorrarPlato(int id)
        {
            platos? plato = (from e in _restauranteContext.platos
                               where e.platoId == id
                               select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }
            _restauranteContext.Attach(plato);
            _restauranteContext.Remove(plato);
            _restauranteContext.SaveChanges();
            return Ok(plato);
        }

        [HttpGet]
        [Route("FilterByName/{name}")]
        public IActionResult GetByName(string name)
        {
            List<platos> lstPlatos = (from e in _restauranteContext.platos
                                         where e.nombrePlato.Contains(name)
                                         select e).ToList();
            if (lstPlatos == null)
            {
                return NotFound();
            }

            return Ok(lstPlatos);
        }
    }
}
