
using System;
using System.Threading.Tasks;
using app.Model;
using app.Servicios.Empleados;
using app.Servicios.Nominas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {

    [Route("api/payroll")]
	[ApiController]
	[EnableCors("PermitirOrigenes")]
    public class NominaController : ControllerBase {

        private readonly IServicioNomina _servicioNomina;
        public NominaController(IServicioNomina servicioNomina){
			_servicioNomina = servicioNomina;
		}

       
		[HttpGet] public async Task<IActionResult> Get(){
			 try
			{
				var nomina = await _servicioNomina.GetAll();
				if (nomina.Count > 0)
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

		[HttpGet("{id}")] public async Task<IActionResult> Get(int id){
			try
			{
				var nomina = await _servicioNomina.Get(id);
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
 		
		[HttpPost] public async Task <IActionResult> Post ([FromBody] Nomina data){
            
            try
			{
				var empleado = await _servicioNomina.Add(data);
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

		[HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] Nomina data){
			try
			{
				var nomina = await _servicioNomina.Update(id, data);
				if (nomina)
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
				var nomina = await _servicioNomina.Delete(id);
				if (nomina)
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