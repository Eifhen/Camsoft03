
using System;
using System.Threading.Tasks;
using app.Model;
using app.Servicios.Empleados;
using app.Servicios.Nominas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {

    [Route("api/payroll/details")]
	[ApiController]
	[EnableCors("PermitirOrigenes")]
    public class NominaDetalleController : ControllerBase {

        private readonly IServicioNominaDetalle _servicioNominaDetalle;
        public NominaDetalleController(IServicioNominaDetalle servicioNominaDetalle){
			_servicioNominaDetalle = servicioNominaDetalle;
		}

        [HttpGet("{id}")] public async Task<IActionResult> Get(int id){
			try
			{
				var nomina = await _servicioNominaDetalle.GetAll(id);
				if (nomina != null)
				{
					return Ok(nomina);
				}

				return NotFound("No data");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        

    }    
}