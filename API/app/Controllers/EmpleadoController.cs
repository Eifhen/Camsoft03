

using System;
using System.Threading.Tasks;
using app.Model;
using app.Servicios.Empleados;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {

    [Route("api/employees")]
	[ApiController]
	[EnableCors("PermitirOrigenes")]
    public class EmpleadoController : ControllerBase {

        private readonly IServicioEmpleado _servicioEmpleado;
        public EmpleadoController(IServicioEmpleado servicioEmpleado){
			_servicioEmpleado = servicioEmpleado;
		}

       
		[HttpGet] public async Task<IActionResult> Get(){

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

		[HttpGet("{id}")] public async Task<IActionResult> Get(int id){
			try
			{
				var empleado = await _servicioEmpleado.Get(id);
				if (empleado != null)
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
 		
		[HttpPost] public async Task <IActionResult> Post ([FromBody] Empleado data){
            
            try
			{
				var empleado = await _servicioEmpleado.Add(data);
				if (empleado)
				{
					return Ok("Success");
				}

				return NotFound("Error");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
        } 

		[HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] Empleado data){
			try
			{
				var empleado = await _servicioEmpleado.Update(id, data);
				if (empleado)
				{
					return Ok("Updated");
				}

				return NotFound("Failed to update");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")] public async Task<IActionResult> Delete(int id){
			try
			{
				var empleado = await _servicioEmpleado.Delete(id);
				if (empleado)
				{
					return Ok("Deleted");
				}

				return NotFound("Failed to delete");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }    
}