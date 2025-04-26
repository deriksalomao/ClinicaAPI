using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Context;
using ProjetoClinica.Models;
using ProjetoClinica.Services.Interfaces;
using ProjetoClinica.ViewModels;

namespace ProjetoClinica.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPacienteService _pacienteService;

        public PacienteController(AppDbContext context, IPacienteService pacienteService)
        {
            _context = context;
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            try
            {
                IEnumerable<Paciente> pacientes = await _pacienteService.GetPacientes();

                return Ok(pacientes);
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
                Paciente paciente = await _pacienteService.GetPacienteById(id);

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente([FromBody] PacienteVM novoPaciente)
        {
            if (novoPaciente == null)
                return BadRequest("Dados do paciente não podem ser nulos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Paciente paciente = await _pacienteService.CreatePaciente(novoPaciente);
                return CreatedAtAction(nameof(GetPacienteById), new { id = paciente.Id }, paciente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar paciente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(Guid id)
        {
            try
            {
                await _pacienteService.DeletePaciente(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(Guid id, [FromBody] PacienteVM pacienteVM)
        {
            try
            {
                if (pacienteVM == null) return BadRequest("Esse paciente não pode ser nulo!");

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _pacienteService.UpdatePaciente(id, pacienteVM);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema: {ex.Message}");
            }
        }
    }
}