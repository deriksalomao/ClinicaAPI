using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Context;
using ProjetoClinica.Models;

namespace ProjetoClinica.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> GetPacientes()
        {
            try
            {
                IEnumerable<Paciente> pacientesRetorno = _context.Pacientes;

                return Ok(pacientesRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPacienteById(Guid id)
        {
            try
            {
                Paciente? pacienteRetorno = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

                if (pacienteRetorno == null) return NotFound();

                return Ok(pacienteRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente([FromBody] Paciente novoPaciente)
        {
            try
            {   
                _context.Pacientes.Add(novoPaciente);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPacienteById), new { id = novoPaciente.Id }, novoPaciente);
            }
            catch (Exception ex) 
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(Guid id)
        {
            try
            {
                Paciente? pacienteASerDeletado = await _context.Pacientes.FindAsync(id);

                if (pacienteASerDeletado == null) return NotFound("Paciente não encontrado");

                _context.Pacientes.Remove(pacienteASerDeletado);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }
    }
}