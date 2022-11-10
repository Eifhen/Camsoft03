using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using app.Servicios.Empleados;
using Microsoft.AspNetCore.Cors;

namespace app.Controllers
{
	[Route("api/pruebas")]
	[ApiController]
	[EnableCors("PermitirOrigenes")]
	public class PruebaController : ControllerBase
	{

		private readonly IServicioEmpleado _servicioEmpleado;
		public PruebaController(IServicioEmpleado servicioEmpleado)
		{
			_servicioEmpleado = servicioEmpleado;
		}

		[HttpGet] public async Task<IActionResult> Get([FromQuery] QueryStringFormatt query)
		{

			var args = this.HttpContext.Request.Query;
			try
			{
				var empleado = await _servicioEmpleado.GetAll();
				if (empleado.Count > 0)
				{
					return Ok(empleado);
				}

				return NotFound("No data");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}


	public class QueryStringFormatt
	{
		public short skip { get; set; }

		public short take { get; set; }

		public bool requireTotalCount { get; set; }
	}

}
