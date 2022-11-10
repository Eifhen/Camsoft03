using System;
using System.Threading.Tasks;
using app.Model;
using app.Servicios.Departamentos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {

    [Route("api/departments")]
	[ApiController]
	[EnableCors("PermitirOrigenes")]
    public class DepartamentoController : ControllerBase {

        private readonly IServicioDepartamento _servicioDepartamento;
        public DepartamentoController(IServicioDepartamento servicioDepartamento){
			_servicioDepartamento = servicioDepartamento;
		}

        
		[HttpGet] public async Task<IActionResult> Get(){
			 try
			{
				var departamentos = await _servicioDepartamento.GetAll();
				if (departamentos.Count > 0)
				{
					return Ok(departamentos);
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
				var departamento = await _servicioDepartamento.Get(id);
				if (departamento != null)
				{
					return Ok(departamento);
				}

				return NotFound("No data");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost] public async Task <IActionResult> Post ([FromBody] Departamento data){
            
            try
			{
				var departamento = await _servicioDepartamento.Add(data);
				if (departamento)
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

		[HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] Departamento data){
			try
			{
				var departamento = await _servicioDepartamento.Update(id, data);
				if (departamento)
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
				var departamento = await _servicioDepartamento.Delete(id);
				if (departamento)
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